using System.Text.Json;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using ProjectName.WebApp;
using Serilog;
using ITL.Web.HealthChecks;
using System;
using ITL.Web.Middlewares;
using Prometheus;

namespace ProjectName.Web
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var startsDate = DateTimeOffset.UtcNow;
            var builder = WebApplication.CreateBuilder(args);

            builder.WebHost
                .UseKestrel((context, option) =>
                {
                    option.Configure(context.Configuration.GetSection("Kestrel"));
                })
                .ConfigureAppConfiguration((context, configBuilder) =>
                {
                    var env = context.HostingEnvironment.EnvironmentName;

                    configBuilder.AddJsonFile("appsettings.json");
                    configBuilder.AddJsonFile($"appsettings.{env}.json", optional: true);
                    configBuilder.AddJsonFile($"Configs/connectionStrings.{env}.json", optional: true);
                    configBuilder.AddJsonFile($"Configs/serilog.{env}.json", optional: true);

                    configBuilder.AddEnvironmentVariables();
                });

            builder.Host.UseSerilog((ctx, conf) => conf.ReadFrom.Configuration(ctx.Configuration));

            builder.Services.ConfigureApp();

            builder.Services
                .AddControllers(opt =>
                {
                    opt.Filters.Add(SupportRestfulApi.Instance);
                })
                .AddJsonOptions(x =>
                {
                    x.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter(JsonNamingPolicy.CamelCase));
                });

            builder.Services.AddHealthChecks();
            builder.Services.AddHealthCheckPrometheusFormatter();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddResponseCompression();

            builder.Services.AddSwaggerGen(opt =>
            {
                opt.IncludeXmlComments("ProjectName.WebApp.xml");
                opt.IncludeXmlComments("ProjectName.Contracts.xml");
            });

            var app = builder.Build();

            if (!app.Environment.IsProduction())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseResponseCompression();
            app.UseRouting();
            app.UseHttpMetrics();
            app.UseAuthorization();

            app.UseMiddleware<AppExceptionMiddleware>();
            app.UseMiddleware<ErrorLoggingMiddleware>();

            app.MapControllers();
            app.UseEndpoints(e =>
            {
                e.MapHealthChecksFormatter();
                e.Map("/starts", ctx => ctx.Response.WriteAsync(startsDate.ToString("O")));
                e.MapMetrics();
            });

            app.Run();
        }
    }
}
