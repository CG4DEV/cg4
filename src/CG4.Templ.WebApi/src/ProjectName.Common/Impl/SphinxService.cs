using System.Collections.Generic;
using System.Threading.Tasks;
using Dapper;

namespace ProjectName.Common.Impl
{
    public class SphinxService : ISearchService
    {
        private const int COMMAND_TIMEOUT = 300;

        private readonly ISphinxConnectionFactory _factory;

        public SphinxService(ISphinxConnectionFactory factory)
        {
            _factory = factory;
        }

        public async Task<IEnumerable<long>> SearchAsync(string sql, object param = null)
        {
            using var connection = await _factory.CreateAsync();
            return await connection.QueryAsync<long>(sql, param, commandTimeout: COMMAND_TIMEOUT);
        }

        public async Task<long> SearchByIdAsync(string sql, object param = null)
        {
            using var connection = await _factory.CreateAsync();
            return await connection.QuerySingleOrDefaultAsync<long>(sql, param, commandTimeout: COMMAND_TIMEOUT);
        }
    }
}