using System.Data;
using System.Linq.Expressions;
using ITL.DataAccess;
using ITL.DataAccess.Domain;
using ITL.DataAccess.Poco;
using ITL.DataAccess.Poco.Expressions;

namespace ITL.Impl.Dapper.Crud
{
    /// <summary>
    /// Наследник <see cref="RepositoryDapper"/> и реализация <see cref="ICrudService"/>, <see cref="IAppCrudService"/>
    /// для работы с источником данных.
    /// </summary>
    public class AppCrudService : RepositoryDapper, ICrudService, IAppCrudService
    {
        private readonly ISqlBuilder _sqlBuilder;

        /// <summary>
        /// Создание экземпляра класса <see cref="AppCrudService"/>.
        /// </summary>
        /// <param name="factory">Фабрика, создающая подключение к источнику данных.</param>
        /// <param name="sqlBuilder">Генератор SQL-кода.</param>
        public AppCrudService(IConnectionFactory factory, ISqlBuilder sqlBuilder)
            : base(factory)
        {
            _sqlBuilder = sqlBuilder;
        }

        /// <inheritdoc/>
        public Task<T> GetAsync<T>(
            long id, 
            IDbConnection connection = null, 
            IDbTransaction transaction = null, 
            CancellationToken ct = default)
            where T : class, IEntityBase, new()
        {
            var exprSql = _sqlBuilder.GenerateSql<T>(x => x.Where(w => w.Id == id));
            var sql = _sqlBuilder.Serialize(exprSql);

            return QueryAsync<T>(sql, connection: connection, transaction: transaction, ct: ct);
        }

        /// <inheritdoc/>
        public Task<T> GetAsync<T>(
            Expression<Action<IClassSqlOptions<T>>> predicate, 
            IDbConnection connection = null, 
            IDbTransaction transaction = null, 
            CancellationToken ct = default)
            where T : class, IEntityBase, new()
        {
            var sql = _sqlBuilder.GetAll(predicate);

            return QueryAsync<T>(sql, connection: connection, transaction: transaction, ct: ct);
        }

        /// <inheritdoc/>
        public Task<TResult> GetAsync<TEntity, TResult>(
            long id, 
            IDbConnection connection = null, 
            IDbTransaction transaction = null, 
            CancellationToken ct = default)
            where TEntity : class, IEntityBase, new()
            where TResult : class, new()
        {
            var exprSql = _sqlBuilder.GenerateSql<TEntity>(x => x.Where(w => w.Id == id));
            var sql = _sqlBuilder.Serialize(exprSql);

            return QueryAsync<TResult>(sql, connection: connection, transaction: transaction, ct: ct);
        }

        /// <inheritdoc/>
        public Task<TResult> GetAsync<TEntity, TResult>(
            Expression<Action<IClassSqlOptions<TEntity>>> predicate, 
            IDbConnection connection = null, 
            IDbTransaction transaction = null, 
            CancellationToken ct = default)
            where TEntity : class, IEntityBase, new()
            where TResult : class, new()
        {
            var sql = _sqlBuilder.GetAll(predicate);

            return QueryAsync<TResult>(sql, connection: connection, transaction: transaction, ct: ct);
        }

        /// <inheritdoc/>
        public Task<IEnumerable<T>> GetAllAsync<T>(
            Expression<Action<IClassSqlOptions<T>>> predicate = null, 
            IDbConnection connection = null, 
            IDbTransaction transaction = null, 
            CancellationToken ct = default)
            where T : class, IEntityBase, new()
        {
            var sql = _sqlBuilder.GetAll(predicate);

            return QueryListAsync<T>(sql, connection: connection, transaction: transaction, ct: ct);
        }

        /// <inheritdoc/>
        public Task<IEnumerable<TResult>> GetAllAsync<TEntity, TResult>(
            Expression<Action<IClassSqlOptions<TEntity>>> predicate = null, 
            IDbConnection connection = null, 
            IDbTransaction transaction = null, 
            CancellationToken ct = default)
            where TEntity : class, IEntityBase, new()
            where TResult : class, new()
        {
            var sql = _sqlBuilder.GetAll(predicate);

            return QueryListAsync<TResult>(sql, connection: connection, transaction: transaction, ct: ct);
        }

        /// <inheritdoc/>
        public async Task<T> CreateAsync<T>(
            T entity, 
            IDbConnection connection = null, 
            IDbTransaction transaction = null, 
            CancellationToken ct = default)
            where T : class, IEntityBase, new()
        {
            entity.CreateDate = DateTimeOffset.UtcNow;
            entity.UpdateDate = DateTimeOffset.UtcNow;

            var sql = _sqlBuilder.Insert<T>();
            var identityValue = await QueryAsync<long>(sql, entity, connection, transaction, ct: ct);

            entity.Id = identityValue;

            return entity;
        }

        /// <inheritdoc/>
        public async Task<T> UpdateAsync<T>(
            T entity, 
            IDbConnection connection = null, 
            IDbTransaction transaction = null, 
            CancellationToken ct = default)
            where T : class, IEntityBase, new()
        {
            entity.UpdateDate = DateTimeOffset.UtcNow;

            var sql2 = _sqlBuilder.UpdateById<T>();

            var entityUpdated = await ExecuteAsync(sql2, entity, connection, transaction, ct: ct);

            if (entityUpdated == 0)
            {
                return null;
            }

            return entity;
        }

        /// <inheritdoc/>
        public Task DeleteAsync<T>(
            long id, 
            IDbConnection connection = null, 
            IDbTransaction transaction = null, 
            CancellationToken ct = default)
            where T : class, IEntityBase, new()
        {
            var sql = _sqlBuilder.DeleteById<T>();

            return ExecuteAsync(sql, new { Id = id }, connection, transaction, ct: ct);
        }

        /// <inheritdoc/>
        public Task<PageResult<TEntity>> GetPageAsync<TEntity>(
            int? page, 
            int? take, 
            Expression<Action<IClassSqlOptions<TEntity>>> predicate = null, 
            IDbConnection connection = null, 
            IDbTransaction transaction = null, 
            CancellationToken ct = default)
            where TEntity : class, IEntityBase, new()
        {
            return GetPageAsync<TEntity, TEntity>(page, take, predicate, connection, transaction, ct: ct);
        }

        /// <inheritdoc/>
        public async Task<PageResult<TResult>> GetPageAsync<TEntity, TResult>(
            int? page, 
            int? take, 
            Expression<Action<IClassSqlOptions<TEntity>>> predicate = null, 
            IDbConnection connection = null, 
            IDbTransaction transaction = null, 
            CancellationToken ct = default)
            where TEntity : class, IEntityBase, new()
            where TResult : class, new()
        {
            take = take.HasValue ? take : 25;
            page = page > 0 ? page : 0;
            var offset = page * take;

            var exprSql = _sqlBuilder.GenerateSql(predicate);

            exprSql.Limit = take;
            exprSql.Offset = offset;

            var sql = _sqlBuilder.Serialize(exprSql);

            exprSql.Select = new();
            exprSql.Limit = null;
            exprSql.Offset = null;
            exprSql.OrderBy = new();

            var exprFunc = new ExprFunctionCount();

            exprFunc.Parametrs.Add(new ExprStar());
            exprSql.Select.Add(exprFunc);

            var sqlCount = _sqlBuilder.Serialize(exprSql);

            var list = await QueryListAsync<TResult>(sql, null, connection, transaction, ct);
            var count = await QueryAsync<int>(sqlCount, null, connection, transaction, ct);

            return new PageResult<TResult>
            {
                Data = list,
                Page = page.Value,
                FilteredCount = count,
                Count = count,
            };
        }
    }
}
