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

        Task<T> GetAsync<T>(IEntity context, Expression<Action<IClassSqlOptions<T>>> predicate = null, IDbConnection connection = null, IDbTransaction transaction = null)
            where T : class, IEntityBase, new();

        Task<IEnumerable<T>> GetAllAsync<T>(Expression<Action<IClassSqlOptions<T>>> predicate = null, IDbConnection connection = null, IDbTransaction transaction = null)
            where T : class, IEntityBase, new();

        Task<TResult> GetAsync<TEntity, TResult>(IEntity context, Expression<Action<IClassSqlOptions<TEntity>>> predicate = null, IDbConnection connection = null, IDbTransaction transaction = null)
            where TEntity : class, IEntityBase, new()
            where TResult : class, new();

        Task<IEnumerable<TResult>> GetAllAsync<TEntity, TResult>(Expression<Action<IClassSqlOptions<TEntity>>> predicate = null, IDbConnection connection = null, IDbTransaction transaction = null)
            where TEntity : class, IEntityBase, new()
            where TResult : class, new();

        Task<T> CreateAsync<T>(object context, IDbConnection connection = null, IDbTransaction transaction = null)
            where T : class, IEntityBase, new();

        Task<EntityContext> UpdateAsync<T>(IEntity context, IDbConnection connection = null, IDbTransaction transaction = null)
            where T : class, IEntityBase, new();

        Task<EntityContext> DeleteAsync<T>(IEntity context, IDbConnection connection = null, IDbTransaction transaction = null)
            where T : class, IEntityBase, new();
    }
}
