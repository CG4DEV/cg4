using System.Data;
using CG4.DataAccess;
using Dapper;

namespace CG4.Impl.Dapper
{
    public class RepositoryDapper : ISqlRepository, ISqlRepositoryAsync, ISqlCrudRepository, ISqlCrudRepositoryAsync
    {
        private readonly int _commandTimeout = 300;
        protected readonly IConnectionFactory _factory;

        /// <summary>
        /// Initializes a new instance of the <see cref="RepositoryDapper"/> class.
        /// </summary>
        /// <param name="factory"></param>
        public RepositoryDapper(IConnectionFactory factory)
        {
            _factory = factory;
        }

        /// <inheritdoc/>
        public T Get<T>(string sql, object param = null, IDbConnection connection = null, IDbTransaction transaction = null)
            where T : class
        {
            if (connection == null)
            {
                using (connection = _factory.Create())
                {
                    return connection.QueryFirstOrDefault<T>(sql, param, commandTimeout: _commandTimeout);
                }
            }
            else
            {
                return connection.QueryFirstOrDefault<T>(sql, param, transaction, commandTimeout: _commandTimeout);
            }
        }

        /// <inheritdoc/>
        public async Task<T> GetAsync<T>(string sql, object param = null, IDbConnection connection = null, IDbTransaction transaction = null)
            where T : class
        {
            if (connection == null)
            {
                using (connection = await _factory.CreateAsync())
                {
                    return await connection.QueryFirstOrDefaultAsync<T>(sql, param, commandTimeout: _commandTimeout);
                }
            }
            else
            {
                return await connection.QueryFirstOrDefaultAsync<T>(sql, param, transaction, commandTimeout: _commandTimeout);
            }
        }


        /// <inheritdoc/>
        public IEnumerable<T> GetAll<T>(string sql, object param = null, IDbConnection connection = null, IDbTransaction transaction = null)
            where T : class
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

        /// <inheritdoc/>
        public async Task<IEnumerable<T>> GetAllAsync<T>(string sql, object param = null, IDbConnection connection = null, IDbTransaction transaction = null)
            where T : class
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

        /// <inheritdoc/>
        public IEnumerable<T> Query<T>(string sql, object param = null, IDbConnection connection = null, IDbTransaction transaction = null)
            where T : class
        {
            if (connection == null)
            {
                using (connection = _factory.Create())
                {
                    return connection.Query<T>(sql, param, transaction: transaction);
                }
            }
            else
            {
                return connection.Query<T>(sql, param, transaction: transaction);
            }
        }

        /// <inheritdoc/>
        public async Task<IEnumerable<T>> QueryAsync<T>(string sql, object param = null, IDbConnection connection = null, IDbTransaction transaction = null)
            where T : class
        {
            if (connection == null)
            {
                using (connection = await _factory.CreateAsync())
                {
                    return await connection.QueryAsync<T>(sql, param, transaction: transaction);
                }
            }
            else
            {
                return await connection.QueryAsync<T>(sql, param, transaction: transaction);
            }
        }

        public async Task<T> QuerySingleOrDefaultAsync<T>(string sql, object param = null, IDbConnection connection = null, IDbTransaction transaction = null)
        {
            if (connection == null)
            {
                using (connection = await _factory.CreateAsync())
                {
                    return await connection.QueryFirstOrDefaultAsync<T>(sql, param, transaction: transaction);
                }
            }
            else
            {
                return await connection.QueryFirstOrDefaultAsync<T>(sql, param, transaction: transaction);
            }
        }
    }
}