using CG4.Web.HealthChecks.Visitors;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Diagnostics.HealthChecks;

namespace CG4.Web.HealthChecks
{
    public static class HealthCheckExtensions
    {
        public static IEndpointConventionBuilder MapHealthChecksFormatter(this IEndpointRouteBuilder routeBuilder, string pattern = "health")
        {
            var writer = routeBuilder.ServiceProvider.GetRequiredService<IReportWriter>();

            return routeBuilder.MapHealthChecks(pattern, new HealthCheckOptions
            {
                ResultStatusCodes = new Dictionary<HealthStatus, int>
                {
                    { HealthStatus.Healthy, 200 },
                    { HealthStatus.Degraded, 200 },
                    { HealthStatus.Unhealthy, 500 },
                },
                ResponseWriter = writer.WriteAsync
            });
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
    }
}
