using System.Data;
using System.Data.Common;
using System.Linq.Expressions;
using CG4.Impl.EF.Helpers;
using Microsoft.EntityFrameworkCore;

namespace CG4.Impl.EF
{

    public class EfBulkCrudService : IRepository, ISqlRepository, ICrud
    {
        private readonly IDbContextFactory _dbContextFactory;

        public EfBulkCrudService(IDbContextFactory dbContextFactory)
        {
            this._dbContextFactory = dbContextFactory;
        }

        public T Get<T>(string sql, object param = null, IDbConnection connection = null, IDbTransaction transaction = null) where T : class
        {
            using var context = _dbContextFactory.CreateContext(connection as DbConnection);
            return context.Set<T>().FromSqlRaw(sql, SqlHelper.GetSqlParameter(param)).First();
        }

        public IEnumerable<T> GetAll<T>(string sql, object param = null, IDbConnection connection = null, IDbTransaction transaction = null)
            where T : class
        {
            using var context = _dbContextFactory.CreateContext(connection as DbConnection);
            return context.Set<T>().FromSqlRaw(sql, SqlHelper.GetSqlParameter(param));
        }

        public T Get<T>(Expression<Func<T, bool>> predicate, IDbConnection connection = null, IDbTransaction transaction = null) where T : class
        {
            using var context = _dbContextFactory.CreateContext(connection as DbConnection);
            return context.Set<T>().Where(predicate).First();
        }

        public IEnumerable<T> GetAll<T>(Expression<Func<T, bool>> predicate = null, IDbConnection connection = null, IDbTransaction transaction = null)
            where T : class
        {
            using var context = _dbContextFactory.CreateContext(connection as DbConnection);
            IEnumerable<T> result = context.Set<T>().Where(predicate);
            return result;
        }

        public IEnumerable<T> GetAll<T, TKey>(Expression<Func<T, bool>> predicate = null, Func<T, TKey> orderSelector = null, bool isAcending = true, IDbConnection connection = null, IDbTransaction transaction = null)
            where T : class
        {
            using var context = _dbContextFactory.CreateContext(connection as DbConnection);
            IEnumerable<T> result = context.Set<T>().Where(predicate);
            if (orderSelector != null)
            {
                if (isAcending)
                {
                    result = result.OrderBy(orderSelector);
                }
                else
                {
                    result = result.OrderByDescending(orderSelector);
                }
            }
            return result;
        }

        public IEnumerable<T> GetPage<T>(Expression<Func<T, bool>> predicate = null, int page = 0, int resultsPerPage = 10)
            where T : class
        {
            using var context = _dbContextFactory.CreateContext();
            IEnumerable<T> result = context.Set<T>().Where(predicate);
            return result.Skip(page * resultsPerPage).Take(resultsPerPage);
        }

        public IEnumerable<T> GetPage<T, TKey>(Expression<Func<T, bool>> predicate = null, Func<T, TKey> orderSelector = null, bool isAcending = true, int page = 0, int resultsPerPage = 10)
            where T : class
        {
            using var context = _dbContextFactory.CreateContext();
            IEnumerable<T> result = context.Set<T>().Where(predicate);
            if (orderSelector != null)
            {
                if (isAcending)
                {
                    result = result.OrderBy(orderSelector);
                }
                else
                {
                    result = result.OrderByDescending(orderSelector);
                }
            }
            return result.Skip(page * resultsPerPage).Take(resultsPerPage);
        }

        //BULK
        public Task<IEnumerable<T>> DeleteAsync<T>(IEnumerable<T> entities, IDbConnection connection = null, IDbTransaction transaction = null)
            where T : class
        {
            return ExecuteAction(entities, connection, transaction, (context, array) => context.RemoveRange(array));
        }

        public Task<IEnumerable<T>> InsertAsync<T>(IEnumerable<T> entities, IDbConnection connection = null, IDbTransaction transaction = null)
            where T : class
        {
            return ExecuteAction(entities, connection, transaction, async (context, array) => await context.AddRangeAsync(array));
        }

        public Task<IEnumerable<T>> UpdateAsync<T>(IEnumerable<T> entities, IDbConnection connection = null, IDbTransaction transaction = null)
            where T : class
        {
            return ExecuteAction(entities, connection, transaction, (context, array) => context.UpdateRange(array));
        }

        private async Task<IEnumerable<T>> ExecuteAction<T>(IEnumerable<T> entites, IDbConnection connection, IDbTransaction transaction, Action<DbContext, T[]> action)
        {
            using var context = _dbContextFactory.CreateContext(connection as DbConnection);
            await context.Database.UseTransactionAsync(transaction as DbTransaction);
            var result = entites.ToArray();
            action(context, result);
            await context.SaveChangesAsync();
            return result;
        }

        public dynamic Insert<T>(T entity, IDbConnection connection = null, IDbTransaction transaction = null) where T : class
        {
            using var context = _dbContextFactory.CreateContext(connection as DbConnection);
            context.Database.UseTransaction(transaction as DbTransaction);
            context.Add(entity);
            context.SaveChanges();
            return entity;
        }

        public bool Update<T>(T entity, IDbConnection connection = null, IDbTransaction transaction = null) where T : class
        {
            using var context = _dbContextFactory.CreateContext(connection as DbConnection);
            context.Database.UseTransaction(transaction as DbTransaction);
            context.Update(entity);
            context.SaveChanges();
            return true;
        }

        public bool Delete<T>(T entity, IDbConnection connection = null, IDbTransaction transaction = null) where T : class
        {
            using var context = _dbContextFactory.CreateContext(connection as DbConnection);
            context.Database.UseTransaction(transaction as DbTransaction);
            context.Remove(entity);
            context.SaveChanges();
            return true;
        }

        public IEnumerable<T> Query<T>(string sql, object param = null, IDbConnection connection = null, IDbTransaction transaction = null) where T : class
        {
            using var context = _dbContextFactory.CreateContext(connection as DbConnection);
            return context.Set<T>().FromSqlRaw(sql, SqlHelper.GetSqlParameter(param));
        }

        public int Execute(string sql, object param = null, IDbConnection connection = null, IDbTransaction transaction = null)
        {
            using var context = _dbContextFactory.CreateContext(connection as DbConnection);
            return context.Database.ExecuteSqlRaw(sql, SqlHelper.GetSqlParameter(param));
        }
    }
}
