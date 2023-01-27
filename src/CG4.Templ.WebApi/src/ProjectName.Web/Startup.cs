using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using ProjectName.Common.Impl;
using ProjectName.WebApp;

namespace ProjectName.Web
{
    public class Startup
    {
        public IWebHostEnvironment WebHostEnvironment { get; }

        public IConfiguration Configuration { get; }

        public Startup(IWebHostEnvironment env, IConfiguration config)
        {
            WebHostEnvironment = env;
            Configuration = config;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddHealthChecks()
                .AddCheck<ReadyHealthCheck>("Ready check");
            
            services
                .AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
                .AddCookie(options =>
                {
                    options.Events.OnRedirectToLogin = context =>
                    {
                        context.Response.StatusCode = 401;
                        return Task.CompletedTask;
                    };
                });
            
            services.AddControllers(o =>
            {
                o.Filters.Add(SupportRestfulApi.Instance);
            });

            services.AddMvc();
            services.AddRouting(options => options.LowercaseUrls = true);
            services.AddEndpointsApiExplorer();

            services.AddSwaggerGen(c =>
            {
                c.OrderActionsBy(description => $"{description.ActionDescriptor.RouteValues["controller"]}_{description.HttpMethod}");
            });

            AppDIConfigure.Configure(services);
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();
            app.UseSwagger();
            app.UseSwaggerUI();

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseMiddleware<AppExceptionMiddleware>();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapHealthChecks("health", new HealthCheckOptions
                {
                    Predicate = _ => false
                });
                
                endpoints.MapHealthChecks("ready", new HealthCheckOptions
                {
                    Predicate = hc => hc.Name == "Ready check"
                });
                
                endpoints.MapControllers();
                endpoints.MapRazorPages();
            });
        }
    }
}
