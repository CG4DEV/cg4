using System.Data;
using System.Linq.Expressions;

namespace CG4.DataAccess
{
    public interface IRepository
    {
        T Get<T>(Expression<Func<T, bool>> predicate, IDbConnection connection = null, IDbTransaction transaction = null)
            where T : class;

        IEnumerable<T> GetAll<T>(Expression<Func<T, bool>> predicate = null, IDbConnection connection = null, IDbTransaction transaction = null)
            where T : class;

        IEnumerable<T> GetAll<T, TKey>(Expression<Func<T, bool>> predicate = null, Func<T, TKey> orderSelector = null, bool isAcending = true, IDbConnection connection = null, IDbTransaction transaction = null)
            where T : class;

        IEnumerable<T> GetPage<T>(Expression<Func<T, bool>> predicate = null, int page = 0, int resultsPerPage = 10)
            where T : class;

        IEnumerable<T> GetPage<T, TKey>(Expression<Func<T, bool>> predicate = null, Func<T, TKey> orderSelector = null, bool isAcending = true, int page = 0, int resultsPerPage = 10)
            where T : class;
    }
}