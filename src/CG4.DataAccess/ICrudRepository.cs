using System.Data;

namespace CG4.DataAccess
{
    public interface ICrudRepository
    {
        dynamic Insert<T>(T entity, IDbConnection connection = null, IDbTransaction transaction = null)
            where T : class;

        bool Update<T>(T entity, IDbConnection connection = null, IDbTransaction transaction = null)
            where T : class;

        bool Delete<T>(T entity, IDbConnection connection = null, IDbTransaction transaction = null)
            where T : class;
    }
}
