using System.Data;
using ITL.DataAccess;
using Npgsql;

namespace ITL.Impl.Dapper.PostgreSql
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
        /// <param name="connectionString">Строка подключения к БД PostgreSQL.</param>
        public ConnectionFactoryPostgreSQL(IConnectionSettings connectionSettings)
        {
            _connectionString = connectionSettings.ConnectionString;
        }

        public IDbConnection Create()
        {
            return Create(_connectionString);
        }

        public IDbConnection Create(string connectionString)
        {
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
            NpgsqlConnection dbConnection = new NpgsqlConnection(connectionString);
            await dbConnection.OpenAsync().ConfigureAwait(false);
            return dbConnection;
        }
    }
}
