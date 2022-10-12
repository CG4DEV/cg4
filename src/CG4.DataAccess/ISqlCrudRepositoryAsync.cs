using System.Data;

namespace CG4.DataAccess
{
    public interface ISqlCrudRepositoryAsync
    {
        Task<IEnumerable<T>> QueryAsync<T>(string sql, object param = null, IDbConnection connection = null, IDbTransaction transaction = null)
            where T : class;
        
        Task<T> QuerySingleOrDefaultAsync<T>(string sql, object param = null, IDbConnection connection = null, IDbTransaction transaction = null);
        
        Task<IEnumerable<T>> QueryListAsync<T>(string sql, object param = null, IDbConnection connection = null, IDbTransaction transaction = null);

        Task<int> ExecuteAsync(string sql, object param = null, IDbConnection connection = null, IDbTransaction transaction = null);
    }
}
