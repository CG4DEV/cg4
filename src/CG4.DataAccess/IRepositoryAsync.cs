using System.Data;
using System.Linq.Expressions;

namespace CG4.DataAccess
{
    public interface IRepositoryAsync
    {
        Task<T> GetAsync<T>(Expression<Func<T, bool>> predicate, IDbConnection connection = null, IDbTransaction transaction = null)
            where T : class;

        Task<IEnumerable<T>> GetAllAsync<T>(Expression<Func<T, bool>> predicate = null, IDbConnection connection = null, IDbTransaction transaction = null)
            where T : class;

        Task<IEnumerable<T>> GetAllAsync<T, TKey>(Expression<Func<T, bool>> predicate = null, Func<T, TKey> orderSelector = null, bool isAcending = true, IDbConnection connection = null, IDbTransaction transaction = null)
            where T : class;

        Task<IEnumerable<T>> GetPageAsync<T>(Expression<Func<T, bool>> predicate = null, int page = 0, int resultsPerPage = 10)
            where T : class;

        Task<IEnumerable<T>> GetPageAsync<T, TKey>(Expression<Func<T, bool>> predicate = null, Func<T, TKey> orderSelector = null, bool isAcending = true, int page = 0, int resultsPerPage = 10)
            where T : class;
    }
}
