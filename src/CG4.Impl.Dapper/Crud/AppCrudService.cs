using System.Data;
using System.Linq.Expressions;
using CG4.DataAccess.Domain;
using CG4.Impl.Dapper.Poco;

namespace CG4.Impl.Dapper.Crud
{
    public class AppCrudService : RepositoryDapper, IAppCrudService
    {
        private readonly ISqlBuilder _sqlBuilder;

        public AppCrudService(IConnectionFactory factory, ISqlBuilder sqlBuilder)
            : base(factory)
        {
            _sqlBuilder = sqlBuilder;
        }

        public Task<T> GetAsync<T>(long id, IDbConnection connection = null, IDbTransaction transaction = null)
            where T : class, IEntityBase, new()
        {
            var exprSql = _sqlBuilder.GenerateSql<T>(x => x.Where(w => w.Id == id));
            var sql = _sqlBuilder.Serialize(exprSql);

            return QuerySingleOrDefaultAsync<T>(sql, connection: connection, transaction: transaction);
        }

        public Task<T> GetAsync<T>(Expression<Action<IClassSqlOptions<T>>> predicate, IDbConnection connection = null, IDbTransaction transaction = null)
            where T : class, IEntityBase, new()
        {
            var sql = _sqlBuilder.GetAll(predicate);

            return QuerySingleOrDefaultAsync<T>(sql, connection: connection, transaction: transaction);
        }

        public Task<TResult> GetAsync<TEntity, TResult>(long id, IDbConnection connection = null, IDbTransaction transaction = null)
            where TEntity : class, IEntityBase, new()
            where TResult : class, new()
        {
            var exprSql = _sqlBuilder.GenerateSql<TEntity>(x => x.Where(w => w.Id == id));
            var sql = _sqlBuilder.Serialize(exprSql);

            return QuerySingleOrDefaultAsync<TResult>(sql, connection: connection, transaction: transaction);
        }

        public Task<TResult> GetAsync<TEntity, TResult>(Expression<Action<IClassSqlOptions<TEntity>>> predicate, IDbConnection connection = null, IDbTransaction transaction = null)
            where TEntity : class, IEntityBase, new()
            where TResult : class, new()
        {
            var sql = _sqlBuilder.GetAll(predicate);

            return QuerySingleOrDefaultAsync<TResult>(sql, connection: connection, transaction: transaction);
        }

        public Task<IEnumerable<T>> GetAllAsync<T>(Expression<Action<IClassSqlOptions<T>>> predicate = null, IDbConnection connection = null, IDbTransaction transaction = null)
            where T : class, IEntityBase, new()
        {
            var sql = _sqlBuilder.GetAll(predicate);

            return QueryAsync<T>(sql, connection: connection, transaction: transaction);
        }

        public Task<IEnumerable<TResult>> GetAllAsync<TEntity, TResult>(Expression<Action<IClassSqlOptions<TEntity>>> predicate = null, IDbConnection connection = null, IDbTransaction transaction = null)
            where TEntity : class, IEntityBase, new()
            where TResult : class, new()
        {
            var sql = _sqlBuilder.GetAll(predicate);

            return QueryAsync<TResult>(sql, connection: connection, transaction: transaction);
        }

        public async Task<T> CreateAsync<T>(T entity, IDbConnection connection = null, IDbTransaction transaction = null)
            where T : class, IEntityBase, new()
        {
            entity.CreateDate = DateTimeOffset.UtcNow;
            entity.UpdateDate = DateTimeOffset.UtcNow;

            var sql = _sqlBuilder.Insert<T>();
            var identityValue = await QuerySingleOrDefaultAsync<long>(sql, entity, connection, transaction);

            entity.Id = identityValue;

            return entity;
        }

        public async Task<T> UpdateAsync<T>(T entity, IDbConnection connection = null, IDbTransaction transaction = null)
            where T : class, IEntityBase, new()
        {
            entity.UpdateDate = DateTimeOffset.UtcNow;

            var sql2 = _sqlBuilder.UpdateById<T>();

            var entityUpdated = await ExecuteAsync(sql2, entity, connection, transaction);

            if (entityUpdated == 0)
            {
                return null;
            }

            return entity;
        }

        public Task DeleteAsync<T>(long id, IDbConnection connection = null, IDbTransaction transaction = null)
            where T : class, IEntityBase, new()
        {
            var sql = _sqlBuilder.DeleteById<T>();

            return ExecuteAsync(sql, new { Id = id }, connection, transaction);
        }
    }
}
