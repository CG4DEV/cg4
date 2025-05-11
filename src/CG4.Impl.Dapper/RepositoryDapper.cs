using System.Data;
using CG4.DataAccess;
using CG4.DataAccess.Exceptions;
using Dapper;

namespace CG4.Impl.Dapper
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

        /// <summary>
        /// Query
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="sql"></param>
        /// <param name="param"></param>
        /// <param name="connection"></param>
        /// <param name="transaction"></param>
        /// <returns></returns>
        /// <exception cref="CG4SqlException"></exception>
        public T? Query<T>(string sql, object? param = null, IDbConnection? connection = null, IDbTransaction? transaction = null)
        {
            if (connection == null)
            {
                using (connection = _factory.Create())
                {
                    try
                    {
                        return connection.QuerySingleOrDefault<T>(sql, param, commandTimeout: _commandTimeout);
                    }
                    catch (Exception ex)
                    {
                        throw new CG4SqlException(sql, ex);
                    }
                }
            }
            else
            {
                try
                {
                    return connection.QuerySingleOrDefault<T>(sql, param, transaction, commandTimeout: _commandTimeout);
                }
                catch (Exception ex)
                {
                    throw new CG4SqlException(sql, ex);
                }
            }
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
        /// <exception cref="CG4SqlException"></exception>
        public async Task<T?> QueryAsync<T>(string sql, object? param = null, IDbConnection? connection = null, IDbTransaction? transaction = null)
        {
            if (connection == null)
            {
                using (connection = await _factory.CreateAsync())
                {
                    try
                    {
                        return await connection.QuerySingleOrDefaultAsync<T>(sql, param, commandTimeout: _commandTimeout);
                    }
                    catch (Exception ex)
                    {
                        throw new CG4SqlException(sql, ex);
                    }
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
                    throw new CG4SqlException(sql, ex);
                }
            }
        }

        /// <summary>
        /// QueryList
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="sql"></param>
        /// <param name="param"></param>
        /// <param name="connection"></param>
        /// <param name="transaction"></param>
        /// <returns></returns>
        /// <exception cref="CG4SqlException"></exception>
        public IEnumerable<T> QueryList<T>(string sql, object? param = null, IDbConnection? connection = null, IDbTransaction? transaction = null)
        {
            if (connection == null)
            {
                using (connection = _factory.Create())
                {
                    try
                    {
                        return connection.Query<T>(sql, param, commandTimeout: _commandTimeout);
                    }
                    catch (Exception ex)
                    {
                        throw new CG4SqlException(sql, ex);
                    }
                }
            }
            else
            {
                try
                {
                    return connection.Query<T>(sql, param, transaction, commandTimeout: _commandTimeout);
                }
                catch (Exception ex)
                {
                    throw new CG4SqlException(sql, ex);
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
        /// <exception cref="CG4SqlException"></exception>
        public async Task<IEnumerable<T>> QueryListAsync<T>(string sql, object? param = null, IDbConnection? connection = null, IDbTransaction? transaction = null)
        {
            if (connection == null)
            {
                using (connection = await _factory.CreateAsync())
                {
                    try
                    {
                        return await connection.QueryAsync<T>(sql, param, commandTimeout: _commandTimeout);
                    }
                    catch (Exception ex)
                    {
                        throw new CG4SqlException(sql, ex);
                    }
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
                    throw new CG4SqlException(sql, ex);
                }
            }
        }

        /// <summary>
        /// Execute
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="param"></param>
        /// <param name="connection"></param>
        /// <param name="transaction"></param>
        /// <returns></returns>
        /// <exception cref="CG4SqlException"></exception>
        public int Execute(string sql, object? param = null, IDbConnection? connection = null, IDbTransaction? transaction = null)
        {
            if (connection == null)
            {
                using (connection = _factory.Create())
                {
                    try
                    {
                        return connection.Execute(sql, param, transaction: transaction);
                    }
                    catch (Exception ex)
                    {
                        throw new CG4SqlException(sql, ex);
                    }
                }
            }
            else
            {
                try
                {
                    return connection.Execute(sql, param, transaction: transaction);
                }
                catch (Exception ex)
                {
                    throw new CG4SqlException(sql, ex);
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
        /// <exception cref="CG4SqlException"></exception>
        public async Task<int> ExecuteAsync(string sql, object? param = null, IDbConnection? connection = null, IDbTransaction? transaction = null)
        {
            if (connection == null)
            {
                using (connection = await _factory.CreateAsync())
                {
                    try
                    {
                        return await connection.ExecuteAsync(sql, param, transaction: transaction);
                    }
                    catch (Exception ex)
                    {
                        throw new CG4SqlException(sql, ex);
                    }
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
                    throw new CG4SqlException(sql, ex);
                }
            }
        }
    }
}
