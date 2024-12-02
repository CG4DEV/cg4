using System.Data;
using Dapper;
using ITL.DataAccess;
using ITL.DataAccess.Exceptions;

namespace ITL.Impl.Dapper
{
    /// <summary>
    /// Репозиторий с реализацией <see cref="ISqlRepository"/> и <see cref="ISqlRepositoryAsync"/> для работы с Dapper.
    /// </summary>
    public class RepositoryDapper : ISqlRepository, ISqlRepositoryAsync
    {
        private readonly int _commandTimeout = 300;
        protected readonly IConnectionFactory _factory;

        /// <summary>
        /// Создание экземпляра класса <see cref="RepositoryDapper"/>.
        /// </summary>
        /// <param name="factory">Фабрика, создающая подключение к источнику данных.</param>
        public RepositoryDapper(IConnectionFactory factory)
        {
            _factory = factory;
        }

        /// <inheritdoc/>
        public T Query<T>(
            string sql, 
            object param = null, 
            IDbConnection connection = null,
            IDbTransaction transaction = null)
        {
            T result = default;

            ExecuteInTransaction(
                sql,
                connection,
                transaction,
                (c, t) =>
                {
                    result = c.QuerySingleOrDefault(sql, param, transaction: transaction, commandTimeout: _commandTimeout);
                });

            return result;
        }

        /// <inheritdoc/>
        public async Task<T> QueryAsync<T>(
            string sql, 
            object param = null, 
            IDbConnection connection = null, 
            IDbTransaction transaction = null, 
            CancellationToken ct = default)
        {
            T result = default;
            
            await ExecuteInTransactionAsync(
                sql,
                connection,
                transaction,
                async (c, t) =>
                {
                    var command = new CommandDefinition(sql, param, transaction: transaction, commandTimeout: _commandTimeout, cancellationToken: ct);
                    result = await c.QuerySingleOrDefaultAsync<T>(command);
                });

            return result;
        }

        /// <inheritdoc/>
        public IEnumerable<T> QueryList<T>(
            string sql, 
            object param = null, 
            IDbConnection connection = null, 
            IDbTransaction transaction = null)
        {
            IEnumerable<T> result = default;

            ExecuteInTransaction(
                sql,
                connection,
                transaction,
                (c, t) =>
                {
                    result = c.Query<T>(sql, param, transaction: transaction, commandTimeout: _commandTimeout);
                });

            return result;
        }

        /// <inheritdoc/>
        public async Task<IEnumerable<T>> QueryListAsync<T>(
            string sql, 
            object param = null, 
            IDbConnection connection = null,
            IDbTransaction transaction = null, 
            CancellationToken ct = default)
        {
            IEnumerable<T> result = default;           

            await ExecuteInTransactionAsync(
                sql,
                connection,
                transaction,
                async (c, t) =>
                {
                    var command = new CommandDefinition(sql, param, transaction: transaction, commandTimeout: _commandTimeout, cancellationToken: ct);
                    result = await c.QueryAsync<T>(command);
                });

            return result;
        }

        /// <inheritdoc/>
        public int Execute(
            string sql, 
            object param = null, 
            IDbConnection connection = null, 
            IDbTransaction transaction = null)
        {
            int result = default;

            ExecuteInTransaction(
                sql,
                connection,
                transaction,
                (c, t) =>
                {
                    result = c.Execute(sql, param, transaction: transaction, commandTimeout: _commandTimeout);
                });

            return result;
        }

        /// <inheritdoc/>
        public async Task<int> ExecuteAsync(
            string sql, 
            object param = null, 
            IDbConnection connection = null, 
            IDbTransaction transaction = null, 
            CancellationToken ct = default)
        {
            int result = default;
            
            await ExecuteInTransactionAsync(
                sql,
                connection,
                transaction,
                async (c, t) =>
                {
                    var command = new CommandDefinition(sql, param, transaction: transaction, commandTimeout: _commandTimeout, cancellationToken: ct);
                    result = await c.ExecuteAsync(command);
                });

            return result;
        }

        private void ExecuteInTransaction(
            string sql,
            IDbConnection connection,
            IDbTransaction transaction,
            Action<IDbConnection, IDbTransaction> f)
        {
            var disposing = false;

            try
            {
                if (connection is null)
                {
                    disposing = true;
                    connection = _factory.Create();
                    transaction = connection.BeginTransaction();
                }

                try
                {
                    f(connection, transaction);
                }
                catch (Exception ex)
                {
                    throw new ITLSqlException(sql, ex);
                }

                if (disposing)
                {
                    transaction.Commit();
                }
            }
            finally
            {
                if (disposing)
                {
                    connection?.Dispose();
                    transaction?.Dispose();
                }
            }
        }

        private async Task ExecuteInTransactionAsync(
            string sql,
            IDbConnection connection,
            IDbTransaction transaction,
            Func<IDbConnection, IDbTransaction, ValueTask> f)
        {
            var disposing = false;

            try
            {
                if (connection is null)
                {
                    disposing = true;
                    connection = await _factory.CreateAsync();
                    transaction = connection.BeginTransaction();
                }

                try
                {
                    await f(connection, transaction);
                }
                catch (Exception ex)
                {
                    throw new ITLSqlException(sql, ex);
                }               

                if (disposing)
                {
                    transaction.Commit();
                }
            }
            finally
            {
                if (disposing)
                {
                    connection?.Dispose();
                    transaction?.Dispose();
                }
            }
        }
    }
}
