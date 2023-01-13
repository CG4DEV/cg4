using System;
using System.Data;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace ProjectName.Common.Impl
{
    public class MySqlConnectionFactory : ISphinxConnectionFactory
    {
        private readonly ISphinxConnectionString _connectionString;

        public MySqlConnectionFactory(ISphinxConnectionString connectionString)
        {
            _connectionString = connectionString ?? throw new ArgumentException(nameof(connectionString));
        }
        
        public IDbConnection Create(string connectionString = null)
        {
            var dbConnection = new MySqlConnection(connectionString ?? _connectionString.SphinxConnectionString);
            dbConnection.Open();
            return dbConnection;
        }

        public async Task<IDbConnection> CreateAsync(string connectionString = null)
        {
            var dbConnection = new MySqlConnection(connectionString ?? _connectionString.SphinxConnectionString);
            await dbConnection.OpenAsync();
            return dbConnection;
        }
    }
}