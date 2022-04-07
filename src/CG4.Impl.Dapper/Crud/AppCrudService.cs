using System.Data;
using System.Linq.Expressions;
using CG4.DataAccess.Domain;
using CG4.Impl.Dapper.Poco;

namespace CG4.Impl.Dapper.Crud
{
    public class AppCrudService : RepositoryDapper
    {
        private readonly ISqlBuilder _sqlBuilder;

        public AppCrudService(IConnectionFactory factory, ISqlBuilder sqlBuilder)
            : base(factory)
        {
            _sqlBuilder = sqlBuilder;
        }

        public Task<T> GetAsync<T>(T entity, Expression<Action<IClassSqlOptions<T>>> predicate = null, IDbConnection connection = null, IDbTransaction transaction = null)
            where T : class, IEntity, IEntityBase, new()
        {
            var sql = _sqlBuilder.GetById(predicate);

            return base.QuerySingleOrDefaultAsync<T>(sql, entity, connection, transaction);
        }

        public Task<IEnumerable<T>> GetAllAsync<T>(Expression<Action<IClassSqlOptions<T>>> predicate = null, IDbConnection connection = null, IDbTransaction transaction = null)
            where T : class, IEntityBase, new()
        {
            var sql = _sqlBuilder.GetAll(predicate);

            // TODO : сделать проброс через параметры
            return base.QueryAsync<T>(sql, null, connection, transaction);
        }

        public async Task<T> CreateAsync<T>(T entity, IDbConnection connection = null, IDbTransaction transaction = null)
            where T : class, IEntityBase, new()
        {
            entity.CreateDate = DateTimeOffset.UtcNow;
            entity.UpdateDate = DateTimeOffset.UtcNow;

            var sql = _sqlBuilder.Insert<T>();

            var identityValue = await this.QuerySingleOrDefaultAsync<long>(sql, entity, connection, transaction);

            // set identity
            var map = PocoHub.GetMap<T>().GetIdentity();
            map.PropertyInfo.SetValue(entity, identityValue);

            return entity;
        }

        public async Task<T> UpdateAsync<T>(T entity, IDbConnection connection = null, IDbTransaction transaction = null)
            where T : class, IEntityBase, new()
        {
            var sql = _sqlBuilder.GetById<T>();

            entity = await this.QuerySingleOrDefaultAsync<T>(sql, entity, connection, transaction);

            if (entity == null)
            {
                return null;
            }

            entity.UpdateDate = DateTimeOffset.UtcNow;

            var sql2 = _sqlBuilder.UpdateById<T>();

            _ = await base.ExecuteAsync(sql2, entity, connection, transaction);

            return entity;
        }

        public async Task<T> DeleteAsync<T>(T entity, IDbConnection connection = null, IDbTransaction transaction = null)
            where T : class, IEntityBase, new()
        {
            var sql = _sqlBuilder.DeleteById<T>();

            _ = await base.ExecuteAsync(sql, entity, connection, transaction);

            return entity;
        }
    }
}
