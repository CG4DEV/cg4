using System.Data;

namespace CG4.DataAccess
{
    public interface ICrudRepositoryAync
    {
        Task<dynamic> InsertAsync<T>(T entity, IDbConnection connection = null, IDbTransaction transaction = null)
            where T : class;

        Task<bool> UpdateAsync<T>(T entity, IDbConnection connection = null, IDbTransaction transaction = null)
            where T : class;

        Task<bool> DeleteAsync<T>(T entity, IDbConnection connection = null, IDbTransaction transaction = null)
            where T : class;
    }
}
