using CG4.Web.HealthChecks.Nodes;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Diagnostics.HealthChecks;

namespace CG4.Web.HealthChecks
{
    internal class ReportWriter : IReportWriter
    {
        private readonly IServiceProvider _serviceProvider;

        public ReportWriter(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public Task WriteAsync(HttpContext context, HealthReport report)
        {
            using var scope = _serviceProvider.CreateScope();
            var nodeVisitor = scope.ServiceProvider.GetRequiredService<INodeVisitor>();
            var nodeReport = NodeReport.From(report);

            nodeReport.Visit(nodeVisitor);

            var reportContent = nodeVisitor.Build();

            return context.Response.WriteAsync(reportContent);
        }
    }
}
