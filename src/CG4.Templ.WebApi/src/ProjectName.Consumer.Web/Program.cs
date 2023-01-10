using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Serilog;

namespace ProjectName.Consumer.Web
{
    public class Program
    {
        static Task Main(string[] args)
        {
            var host = Host.CreateDefaultBuilder(args)
                .ConfigureApp<Startup>(args)
                .Build();

            return host.RunAsync();
        }
    }

    public static class HostBuilderExtension
    {
        public static IHostBuilder ConfigureApp<TStartup>(this IHostBuilder hostBuilder, string[] args)
            where TStartup : class
        {
            hostBuilder
                .ConfigureAppConfiguration((context, configBuilder) =>
                {
                    configBuilder.Sources.Clear();
                    var env = context.HostingEnvironment.EnvironmentName;

                    configBuilder.AddJsonFile("appsettings.json");
                    configBuilder.AddJsonFile($"appsettings.{env}.json");
                    configBuilder.AddJsonFile($"Configs/connectionStrings.{env}.json", optional: true);
                    configBuilder.AddJsonFile($"Configs/serilog.{env}.json", optional: true);

                    configBuilder.AddEnvironmentVariables();

                    if (args != null)
                    {
                        configBuilder.AddCommandLine(args);
                    }
                })
                .UseSerilog((hostingContext, loggerConfiguration) =>
                {
                    loggerConfiguration.ReadFrom.Configuration(hostingContext.Configuration);
                })
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<TStartup>();
                });

            return hostBuilder;
        }
    }
}