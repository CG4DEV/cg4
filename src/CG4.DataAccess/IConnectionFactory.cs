using System.Data;

namespace CG4.DataAccess
{
    /// <summary>
    /// Factory for creating database connections.
    /// Provides methods to create and manage database connections with consistent configuration.
    /// </summary>
    public interface IConnectionFactory
    {
        /// <summary>
        /// Creates a database connection using the default connection configuration.
        /// </summary>
        /// <returns>A new database connection object.</returns>
        IDbConnection Create();

        /// <summary>
        /// Creates a database connection using the specified connection string.
        /// </summary>
        /// <param name="connectionString">The connection string to the data source.</param>
        /// <returns>A new database connection object.</returns>
        IDbConnection Create(string connectionString);

        /// <summary>
        /// Asynchronously creates a database connection using the default connection configuration.
        /// </summary>
        /// <returns>A new database connection object.</returns>
        Task<IDbConnection> CreateAsync();

        /// <summary>
        /// Asynchronously creates a database connection using the specified connection string.
        /// </summary>
        /// <param name="connectionString">The connection string to the data source.</param>
        /// <returns>A new database connection object.</returns>
        Task<IDbConnection> CreateAsync(string connectionString);
    }
}
