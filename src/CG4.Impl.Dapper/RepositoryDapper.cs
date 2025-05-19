using System.Data;
using CG4.DataAccess;
using CG4.DataAccess.Exceptions;
using Dapper;

namespace CG4.Impl.Dapper
{
    /// <summary>
    /// Репозиторий с реализацией <see cref="ISqlRepositoryAsync"/> и <see cref="ISqlRepositoryAsync"/> для работы с Dapper.
    /// </summary>
    public class RepositoryDapper : ISqlRepositoryAsync
    {
        private readonly int _commandTimeout = 300;
        private readonly IConnectionFactory _factory;
        private readonly ICrudServiceLogger _logger;

        /// <summary>
        /// Создание экземпляра класса <see cref="RepositoryDapper"/>.
        /// </summary>
        /// <param name="factory">Фабрика, создающая подключение к источнику данных.</param>
        /// <param name="logger">Логгер SQL-запросов.</param>
        public RepositoryDapper(IConnectionFactory factory, ICrudServiceLogger? logger)
        {
            _factory = factory;
            _logger = logger ?? CrudServiceNullLogger.Instance;
        }

        /// <summary>
        /// QueryAsync
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="sql"></param>
        /// <param name="param"></param>
        /// <param name="connection"></param>
        /// <param name="transaction"></param>
        /// <returns></returns>
        /// <exception cref="SqlExceptionBase"></exception>
        public async Task<T?> QueryAsync<T>(string sql, object? param = null, IDbConnection? connection = null, IDbTransaction? transaction = null)
        {
            _logger.LogQueryStarted<T>(sql);
            
            if (connection == null)
            {
                try
                {
                    using (connection = await _factory.CreateAsync())
                    {
                        try
                        {
                            _logger.LogQueryParameters(param);
                            var result = await connection.QuerySingleOrDefaultAsync<T>(sql, param, commandTimeout: _commandTimeout);
                            return result;
                        }
                        catch (Exception ex)
                        {
                            throw SqlExceptionFactory.Create(sql, param, ex, _commandTimeout);
                        }
                    }
                }
                catch (Exception ex) when (!(ex is SqlExceptionBase))
                {
                    throw SqlExceptionFactory.CreateConnectionException(sql, param, ex);
                }
            }
            else
            {
                try
                {
                    _logger.LogQueryParameters(param);
                    var result = await connection.QuerySingleOrDefaultAsync<T>(sql, param, transaction, commandTimeout: _commandTimeout);
                    return result;
                }
                catch (Exception ex)
                {
                    throw SqlExceptionFactory.Create(sql, param, ex, _commandTimeout);
                }
            }
        }

        /// <summary>
        /// QueryListAsync
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="sql"></param>
        /// <param name="param"></param>
        /// <param name="connection"></param>
        /// <param name="transaction"></param>
        /// <returns></returns>
        /// <exception cref="SqlExceptionBase"></exception>
        public async Task<IEnumerable<T>> QueryListAsync<T>(string sql, object? param = null, IDbConnection? connection = null, IDbTransaction? transaction = null)
        {
            _logger.LogQueryStarted<T>(sql);
            
            if (connection == null)
            {
                try
                {
                    using (connection = await _factory.CreateAsync())
                    {
                        try
                        {
                            _logger.LogQueryParameters(param);
                            var results = await connection.QueryAsync<T>(sql, param, commandTimeout: _commandTimeout);
                            return results;
                        }
                        catch (Exception ex)
                        {
                            throw SqlExceptionFactory.Create(sql, param, ex, _commandTimeout);
                        }
                    }
                }
                catch (Exception ex) when (!(ex is SqlExceptionBase))
                {
                    throw SqlExceptionFactory.CreateConnectionException(sql, param, ex);
                }
            }
            else
            {
                try
                {
                    _logger.LogQueryParameters(param);
                    var results = await connection.QueryAsync<T>(sql, param, transaction, commandTimeout: _commandTimeout);
                    return results;
                }
                catch (Exception ex)
                {
                    throw SqlExceptionFactory.Create(sql, param, ex, _commandTimeout);
                }
            }
        }

        /// <summary>
        /// ExecuteAsync
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="param"></param>
        /// <param name="connection"></param>
        /// <param name="transaction"></param>
        /// <returns></returns>
        /// <exception cref="SqlExceptionBase"></exception>
        public async Task<int> ExecuteAsync(string sql, object? param = null, IDbConnection? connection = null, IDbTransaction? transaction = null)
        {
            _logger.LogQueryStarted(sql);

            if (connection == null)
            {
                try
                {
                    using (connection = await _factory.CreateAsync())
                    {
                        try
                        {
                            _logger.LogQueryParameters(param);
                            var result = await connection.ExecuteAsync(sql, param, transaction: transaction);
                            return result;
                        }
                        catch (Exception ex)
                        {
                            throw SqlExceptionFactory.Create(sql, param, ex, _commandTimeout);
                        }
                    }
                }
                catch (Exception ex) when (!(ex is SqlExceptionBase))
                {
                    throw SqlExceptionFactory.CreateConnectionException(sql, param, ex);
                }
            }
            else
            {
                try
                {
                    _logger.LogQueryParameters(param);
                    var result = await connection.ExecuteAsync(sql, param, transaction: transaction);
                    return result;
                }
                catch (Exception ex)
                {
                    throw SqlExceptionFactory.Create(sql, param, ex, _commandTimeout);
                }
            }
        }
    }
}
