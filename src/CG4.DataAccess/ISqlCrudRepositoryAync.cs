using System.Data;

namespace CG4.DataAccess
{
    public interface ISqlCrudRepositoryAync
    {
        Task<IEnumerable<T>> QueryAsync<T>(string sql, object param = null, IDbConnection connection = null, IDbTransaction transaction = null)
            where T : class;

        Task<int> ExecuteAsync(string sql, object param = null, IDbConnection connection = null, IDbTransaction transaction = null);
    }
}
