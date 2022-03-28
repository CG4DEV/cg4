using System.Data;
using System.Linq.Expressions;

namespace CG4.Impl.EF
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

    public interface ISqlRepository
    {
        T Get<T>(string sql, object param = null, IDbConnection connection = null, IDbTransaction transaction = null)
            where T : class;

        IEnumerable<T> GetAll<T>(string sql, object param = null, IDbConnection connection = null, IDbTransaction transaction = null)
            where T : class;
    }

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

    public interface ISqlRepositoryAsync
    {
        Task<T> GetAsync<T>(string sql, object param = null, IDbConnection connection = null, IDbTransaction transaction = null);

        Task<IEnumerable<T>> GetAllAsync<T>(string sql, object param = null, IDbConnection connection = null, IDbTransaction transaction = null);
    }

    public interface IBulkRepositoryAync
    {
        public Task<IEnumerable<T>> DeleteAsync<T>(IEnumerable<T> entities, IDbConnection connection = null, IDbTransaction transaction = null)
            where T : class;

        public Task<IEnumerable<T>> InsertAsync<T>(IEnumerable<T> entities, IDbConnection connection = null, IDbTransaction transaction = null)
            where T : class;

        public Task<IEnumerable<T>> UpdateAsync<T>(IEnumerable<T> entities, IDbConnection connection = null, IDbTransaction transaction = null)
            where T : class;
    }

    public interface ICrud
    {
        dynamic Insert<T>(T entity, IDbConnection connection = null, IDbTransaction transaction = null)
            where T : class;

        bool Update<T>(T entity, IDbConnection connection = null, IDbTransaction transaction = null)
            where T : class;

        bool Delete<T>(T entity, IDbConnection connection = null, IDbTransaction transaction = null)
            where T : class;

        IEnumerable<T> Query<T>(string sql, object param = null, IDbConnection connection = null, IDbTransaction transaction = null)
            where T : class;

        int Execute(string sql, object param = null, IDbConnection connection = null, IDbTransaction transaction = null);
    }

    public interface ICrudAync
    {
        Task<dynamic> InsertAsync<T>(T entity, IDbConnection connection = null, IDbTransaction transaction = null)
            where T : class;

        Task<bool> UpdateAsync<T>(T entity, IDbConnection connection = null, IDbTransaction transaction = null)
            where T : class;

        Task<bool> DeleteAsync<T>(T entity, IDbConnection connection = null, IDbTransaction transaction = null)
            where T : class;

        Task<IEnumerable<T>> QueryAsync<T>(string sql, object param = null, IDbConnection connection = null, IDbTransaction transaction = null)
            where T : class;

        Task<int> ExecuteAsync(string sql, object param = null, IDbConnection connection = null, IDbTransaction transaction = null);
    }
}