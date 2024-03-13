using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Diagnostics.HealthChecks;

namespace ITL.Web.HealthChecks
{
    internal interface IReportWriter
    {
        Task WriteAsync(HttpContext context, HealthReport report);
    }
}
