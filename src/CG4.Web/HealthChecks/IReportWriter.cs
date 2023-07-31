using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Diagnostics.HealthChecks;

namespace CG4.Web.HealthChecks
{
    internal interface IReportWriter
    {
        Task WriteAsync(HttpContext context, HealthReport report);
    }
}
