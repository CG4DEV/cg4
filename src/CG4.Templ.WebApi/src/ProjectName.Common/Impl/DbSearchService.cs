using System.Collections.Generic;
using System.Threading.Tasks;
using CG4.Impl.Dapper;
using Dapper;

namespace ProjectName.Common.Impl
{
    public class DbSearchService : ISearchService
    {
        private readonly IConnectionFactory _factory;

        public DbSearchService(IConnectionFactory factory)
        {
            _factory = factory;
        }
        
        public async Task<IEnumerable<long>> SearchAsync(string sql, object param = null)
        {
            using var connection = await _factory.CreateAsync();
            return await connection.QueryAsync<long>(sql, param);
        }

        public async Task<long> SearchByIdAsync(string sql, object param = null)
        {
            using var connection = await _factory.CreateAsync();
            return await connection.QuerySingleOrDefaultAsync<long>(sql, param);
        }
    }
}