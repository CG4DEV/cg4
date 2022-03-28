using System.Data;

namespace CG4.DataAccess
{
    public interface ISqlCrudRepository
    {
        IEnumerable<T> Query<T>(string sql, object param = null, IDbConnection connection = null, IDbTransaction transaction = null)
            where T : class;

        int Execute(string sql, object param = null, IDbConnection connection = null, IDbTransaction transaction = null);
    }
}
