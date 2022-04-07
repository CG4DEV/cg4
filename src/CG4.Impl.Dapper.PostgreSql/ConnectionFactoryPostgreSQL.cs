using System.Data;
using Npgsql;

namespace CG4.Impl.Dapper.PostgreSql
{
    public class ConnectionFactoryPostgreSQL : IConnectionFactory
    {
        private readonly string _connectionString;

        public ConnectionFactoryPostgreSQL(string connectionString)
        {
            _connectionString = connectionString;
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