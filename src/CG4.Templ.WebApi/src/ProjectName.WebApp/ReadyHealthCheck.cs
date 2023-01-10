using System.Threading;
using System.Threading.Tasks;
using CG4.Impl.Dapper.Crud;
using Microsoft.Extensions.Diagnostics.HealthChecks;

namespace ProjectName.WebApp
{
    public class ReadyHealthCheck : IHealthCheck
    {
        private readonly ICrudService _crudService;

        public ReadyHealthCheck(ICrudService crudService)
        {
            _crudService = crudService;
        }
        
        public async Task<HealthCheckResult> CheckHealthAsync(
            HealthCheckContext context, 
            CancellationToken cancellationToken = default)
        {
            var result = await _crudService.QuerySingleOrDefaultAsync<int>("SELECT 1");

            return result == 1 
                ? HealthCheckResult.Healthy() 
                : HealthCheckResult.Unhealthy();
        }
    }
}