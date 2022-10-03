using System.Data;
using System.Linq.Expressions;
using CG4.DataAccess.Domain;
using CG4.Impl.Dapper.Poco;

namespace CG4.Impl.Dapper.Crud
{
    public interface IAppCrudService
    {
        Task<T> GetAsync<T>(string sql, object param = null, IDbConnection connection = null, IDbTransaction transaction = null)
            where T : class;

        Task<IEnumerable<T>> GetAllAsync<T>(string sql, object param = null, IDbConnection connection = null, IDbTransaction transaction = null)
            where T : class;

        Task<T> GetAsync<T>(long id, IDbConnection connection = null, IDbTransaction transaction = null)
            where T : class, IEntityBase, new();

        Task<T> GetAsync<T>(Expression<Action<IClassSqlOptions<T>>> predicate, IDbConnection connection = null, IDbTransaction transaction = null)
            where T : class, IEntityBase, new();

        Task<TResult> GetAsync<TEntity, TResult>(long id, IDbConnection connection = null, IDbTransaction transaction = null)
            where TEntity : class, IEntityBase, new()
            where TResult : class, new();

        Task<TResult> GetAsync<TEntity, TResult>(Expression<Action<IClassSqlOptions<TEntity>>> predicate, IDbConnection connection = null, IDbTransaction transaction = null)
            where TEntity : class, IEntityBase, new()
            where TResult : class, new();

        Task<IEnumerable<T>> GetAllAsync<T>(Expression<Action<IClassSqlOptions<T>>> predicate = null, IDbConnection connection = null, IDbTransaction transaction = null)
            where T : class, IEntityBase, new();

        Task<IEnumerable<TResult>> GetAllAsync<TEntity, TResult>(Expression<Action<IClassSqlOptions<TEntity>>> predicate = null, IDbConnection connection = null, IDbTransaction transaction = null)
            where TEntity : class, IEntityBase, new()
            where TResult : class, new();

        Task<T> CreateAsync<T>(T entity, IDbConnection connection = null, IDbTransaction transaction = null)
            where T : class, IEntityBase, new();

        Task<T> UpdateAsync<T>(T entity, IDbConnection connection = null, IDbTransaction transaction = null)
            where T : class, IEntityBase, new();

        Task DeleteAsync<T>(long id, IDbConnection connection = null, IDbTransaction transaction = null)
            where T : class, IEntityBase, new();

        Task<PageResult<TEntity>> GetPageAsync<TEntity>(int page, int take, Expression<Action<IClassSqlOptions<TEntity>>> predicate = null, IDbConnection connection = null, IDbTransaction transaction = null)
            where TEntity : class, IEntityBase, new();

        Task<PageResult<TResult>> GetPageAsync<TEntity, TResult>(int page, int take, Expression<Action<IClassSqlOptions<TEntity>>> predicate = null, IDbConnection connection = null, IDbTransaction transaction = null)
            where TEntity : class, IEntityBase, new()
            where TResult : class, new();
    }
}
