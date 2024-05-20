using ITL.Web.HealthChecks.Visitors;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Diagnostics.HealthChecks;

namespace ITL.Web.HealthChecks
{
    public static class HealthCheckExtensions
    {
        public static IEndpointConventionBuilder MapHealthChecksFormatter(this IEndpointRouteBuilder routeBuilder, string pattern = "health")
        {
            var settings = routeBuilder.ServiceProvider.GetService<IHealthCheckSettings>();
            var writer = routeBuilder.ServiceProvider.GetRequiredService<IReportWriter>();
            var options = BuildHealthCheckOptions(writer);

            var builder = routeBuilder.MapHealthChecks(pattern, options);
            if (settings?.Port is not null)
            {
                builder = builder.RequireHost($"*:{settings.Port}");
            }

            return builder;
        }

        public static IApplicationBuilder UseITLHealthChecks(this IApplicationBuilder applicationBuilder, string path = "/health")
        {
            var settings = applicationBuilder.ApplicationServices.GetService<IHealthCheckSettings>();
            var writer = applicationBuilder.ApplicationServices.GetRequiredService<IReportWriter>();
            var options = BuildHealthCheckOptions(writer);

            return settings?.Port is null
                ? applicationBuilder.UseHealthChecks(path, options)
                : applicationBuilder.UseHealthChecks(path, settings.Port.Value, options);
        }

        public static IServiceCollection AddHealthCheckTextFormatter(this IServiceCollection services)
        {
            return services
                .AddTransient<INodeVisitor, TextNodeVisitor>()
                .AddTransient<IReportWriter, ReportWriter>();
        }

        public static IServiceCollection AddHealthCheckPrometheusFormatter(this IServiceCollection services)
        {
            return services
                .AddTransient<INodeVisitor, PrometheusNodeVisitor>()
                .AddTransient<IReportWriter, ReportWriter>();
        }

        private static HealthCheckOptions BuildHealthCheckOptions(IReportWriter reportWriter)
        {
            return new HealthCheckOptions
            {
                ResultStatusCodes = new Dictionary<HealthStatus, int>
                {
                    { HealthStatus.Healthy, 200 },
                    { HealthStatus.Degraded, 200 },
                    { HealthStatus.Unhealthy, 500 },
                },
                ResponseWriter = reportWriter.WriteAsync
            };
        }
    }
}
