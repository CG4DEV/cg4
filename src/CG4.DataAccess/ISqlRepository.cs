using System.Data;

namespace CG4.DataAccess
{
    public interface ISqlRepository
    {
        T Get<T>(string sql, object param = null, IDbConnection connection = null, IDbTransaction transaction = null)
            where T : class;

        IEnumerable<T> GetAll<T>(string sql, object param = null, IDbConnection connection = null, IDbTransaction transaction = null)
            where T : class;
    }
}
