using System.Data;

namespace CG4.DataAccess
{
    public interface ISqlCrudRepository
    {
        IEnumerable<T> Query<T>(string sql, object param = null, IDbConnection connection = null, IDbTransaction transaction = null)
            where T : class;
        
        T QuerySingleOrDefault<T>(string sql, object param = null, IDbConnection connection = null, IDbTransaction transaction = null);
        
        IEnumerable<T> QueryList<T>(string sql, object param = null, IDbConnection connection = null, IDbTransaction transaction = null);

        int Execute(string sql, object param = null, IDbConnection connection = null, IDbTransaction transaction = null);
    }
}
