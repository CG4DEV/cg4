using System.Data;
using CG4.DataAccess;
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
        
        public T Query<T>(string sql, object param = null, IDbConnection connection = null,
            IDbTransaction transaction = null)
        {
            if (connection == null)
            {
                using (connection = _factory.Create())
                {
                    return connection.QuerySingleOrDefault<T>(sql, param, commandTimeout: _commandTimeout);
                }
            }
            else
            {
                return connection.QuerySingleOrDefault<T>(sql, param, transaction, commandTimeout: _commandTimeout);
            }
        }
        
        public async Task<T> QueryAsync<T>(string sql, object param = null, IDbConnection connection = null, IDbTransaction transaction = null)
        {
            if (connection == null)
            {
                using (connection = await _factory.CreateAsync())
                {
                    return await connection.QuerySingleOrDefaultAsync<T>(sql, param, commandTimeout: _commandTimeout);
                }
            }
            else
            {
                return await connection.QuerySingleOrDefaultAsync<T>(sql, param, transaction, commandTimeout: _commandTimeout);
            }
        }

        public IEnumerable<T> QueryList<T>(string sql, object param = null, IDbConnection connection = null, IDbTransaction transaction = null)
        {
            if (connection == null)
            {
                using (connection = _factory.Create())
                {
                    return connection.Query<T>(sql, param, commandTimeout: _commandTimeout);
                }
            }
            else
            {
                return connection.Query<T>(sql, param, transaction, commandTimeout: _commandTimeout);
            }
        }
        
        public async Task<IEnumerable<T>> QueryListAsync<T>(string sql, object param = null, IDbConnection connection = null,
            IDbTransaction transaction = null)
        {
            if (connection == null)
            {
                using (connection = await _factory.CreateAsync())
                {
                    return await connection.QueryAsync<T>(sql, param, commandTimeout: _commandTimeout);
                }
            }
            else
            {
                return await connection.QueryAsync<T>(sql, param, transaction, commandTimeout: _commandTimeout);
            }
        }

        /// <inheritdoc/>
        public int Execute(string sql, object param = null, IDbConnection connection = null, IDbTransaction transaction = null)
        {
            if (connection == null)
            {
                using (connection = _factory.Create())
                {
                    return connection.Execute(sql, param, transaction: transaction);
                }
            }
            else
            {
                return connection.Execute(sql, param, transaction: transaction);
            }
        }

        /// <inheritdoc/>
        public async Task<int> ExecuteAsync(string sql, object param = null, IDbConnection connection = null, IDbTransaction transaction = null)
        {
            if (connection == null)
            {
                using (connection = await _factory.CreateAsync())
                {
                    return await connection.ExecuteAsync(sql, param, transaction: transaction);
                }
            }
            else
            {
                return await connection.ExecuteAsync(sql, param, transaction: transaction);
            }
        }
    }
}
