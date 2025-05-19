using System.Data;
using CG4.DataAccess;
using Npgsql;

namespace CG4.Impl.Dapper.PostgreSql
{
    /// <summary>
    /// Реализация <see cref="IConnectionFactory"/> для работы с PostgreSQL.
    /// </summary>
    public class ConnectionFactoryPostgreSQL : IConnectionFactory
    {
        private readonly string _connectionString;

        /// <summary>
        /// Создание экземпляра класса <see cref="ConnectionFactoryPostgreSQL"/>.
        /// </summary>
        /// <param name="connectionSettings">Настройки подключения к БД PostgreSQL.</param>
        /// <exception cref="ArgumentNullException">Если connectionSettings - null.</exception>
        public ConnectionFactoryPostgreSQL(IConnectionSettings connectionSettings)
        {
            ArgumentNullException.ThrowIfNull(connectionSettings);
            ArgumentException.ThrowIfNullOrEmpty(connectionSettings.ConnectionString, nameof(connectionSettings.ConnectionString));
            
            _connectionString = connectionSettings.ConnectionString;
        }

        public IDbConnection Create()
        {
            return Create(_connectionString);
        }

        public IDbConnection Create(string connectionString)
        {
            ArgumentException.ThrowIfNullOrEmpty(connectionString, nameof(connectionString));
            
            NpgsqlConnection dbConnection = new NpgsqlConnection(connectionString);
            dbConnection.Open();
            return dbConnection;
        }

        public async Task<IDbConnection> CreateAsync()
        {
            return await CreateAsync(_connectionString);
        }

        public async Task<IDbConnection> CreateAsync(string connectionString)
        {
            ArgumentException.ThrowIfNullOrEmpty(connectionString, nameof(connectionString));
            
            NpgsqlConnection dbConnection = new NpgsqlConnection(connectionString);
            await dbConnection.OpenAsync().ConfigureAwait(false);
            return dbConnection;
        }
    }
}
