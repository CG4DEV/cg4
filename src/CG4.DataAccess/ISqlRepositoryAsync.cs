using System.Data;

namespace CG4.DataAccess
{
    public interface ISqlRepositoryAsync
    {
        Task<T> GetAsync<T>(string sql, object param = null, IDbConnection connection = null, IDbTransaction transaction = null)
            where T : class;

        Task<IEnumerable<T>> GetAllAsync<T>(string sql, object param = null, IDbConnection connection = null, IDbTransaction transaction = null)
            where T : class;
    }
}
