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

        /// <summary>
        /// Создание экземпляра класса <see cref="RepositoryDapper"/>.
        /// </summary>
        /// <param name="factory">Фабрика, создающая подключение к источнику данных.</param>
        public RepositoryDapper(IConnectionFactory factory)
        {
            _factory = factory;
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
            if (connection == null)
            {
                try
                {
                    using (connection = await _factory.CreateAsync())
                    {
                        try
                        {
                            return await connection.QuerySingleOrDefaultAsync<T>(sql, param, commandTimeout: _commandTimeout);
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
                    return await connection.QuerySingleOrDefaultAsync<T>(sql, param, transaction, commandTimeout: _commandTimeout);
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
            if (connection == null)
            {
                try
                {
                    using (connection = await _factory.CreateAsync())
                    {
                        try
                        {
                            return await connection.QueryAsync<T>(sql, param, commandTimeout: _commandTimeout);
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
                    return await connection.QueryAsync<T>(sql, param, transaction, commandTimeout: _commandTimeout);
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
            if (connection == null)
            {
                try
                {
                    using (connection = await _factory.CreateAsync())
                    {
                        try
                        {
                            return await connection.ExecuteAsync(sql, param, transaction: transaction);
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
                    return await connection.ExecuteAsync(sql, param, transaction: transaction);
                }
                catch (Exception ex)
                {
                    throw SqlExceptionFactory.Create(sql, param, ex, _commandTimeout);
                }
            }
        }
    }
}
