using System.Data;

namespace CG4.DataAccess
{
    /// <summary>
    /// Interface for executing raw SQL queries asynchronously.
    /// Provides low-level access to the database for complex queries and operations.
    /// </summary>
    public interface ISqlRepositoryAsync
    {
        /// <summary>
        /// Asynchronously retrieves a single record using a SQL query.
        /// </summary>
        /// <param name="sql">The SQL query string.</param>
        /// <param name="param">
        ///     The query parameters. Can be provided in several formats:
        ///     <list type="number">
        ///         <item>
        ///             <description>
        ///                 Anonymous object: <c>var objectParams = new { First = "A" };</c>
        ///             </description>
        ///         </item>
        ///         <item>
        ///             <description>
        ///                 POCO object: <c>var objectParams = new Context { First = "A" };</c>
        ///             </description>
        ///         </item>
        ///         <item>
        ///             <description>
        ///                 Dictionary: <c>var dictParams = new Dictionary<string, object> { { "First", "A" } };</c>
        ///             </description>
        ///         </item>
        ///         <item>
        ///             <description>
        ///                 Array of objects: <c>var listParams = new Context[] { new() { First = "A" } };</c>
        ///             </description>
        ///         </item>
        ///     </list>
        /// </param>
        /// <param name="connection">An open connection to the data source.</param>
        /// <param name="transaction">The transaction to be used for the operation.</param>
        /// <typeparam name="T">The type to map the result to.</typeparam>
        /// <returns>A single instance of type T or null if not found.</returns>
        Task<T?> QueryAsync<T>(string sql, object? param = null, IDbConnection? connection = null, IDbTransaction? transaction = null);
        
        /// <summary>
        /// Asynchronously retrieves a list of records using a SQL query.
        /// </summary>
        /// <param name="sql">The SQL query string.</param>
        /// <param name="param">
        ///     The query parameters. Can be provided in several formats:
        ///     <list type="number">
        ///         <item>
        ///             <description>
        ///                 Anonymous object: <c>var objectParams = new { First = "A" };</c>
        ///             </description>
        ///         </item>
        ///         <item>
        ///             <description>
        ///                 POCO object: <c>var objectParams = new Context { First = "A" };</c>
        ///             </description>
        ///         </item>
        ///         <item>
        ///             <description>
        ///                 Dictionary: <c>var dictParams = new Dictionary<string, object> { { "First", "A" } };</c>
        ///             </description>
        ///         </item>
        ///         <item>
        ///             <description>
        ///                 Array of objects: <c>var listParams = new Context[] { new() { First = "A" } };</c>
        ///             </description>
        ///         </item>
        ///     </list>
        /// </param>
        /// <param name="connection">An open connection to the data source.</param>
        /// <param name="transaction">The transaction to be used for the operation.</param>
        /// <typeparam name="T">The type to map the results to.</typeparam>
        /// <returns>A list of instances of type T.</returns>
        Task<IEnumerable<T>> QueryListAsync<T>(string sql, object? param = null, IDbConnection? connection = null, IDbTransaction? transaction = null);

        /// <summary>
        /// Asynchronously executes a SQL command.
        /// </summary>
        /// <param name="sql">The SQL command string (INSERT, UPDATE, DELETE, etc.).</param>
        /// <param name="param">
        ///     The command parameters. Can be provided in several formats:
        ///     <list type="number">
        ///         <item>
        ///             <description>
        ///                 Anonymous object: <c>var objectParams = new { First = "A" };</c>
        ///             </description>
        ///         </item>
        ///         <item>
        ///             <description>
        ///                 POCO object: <c>var objectParams = new Context { First = "A" };</c>
        ///             </description>
        ///         </item>
        ///         <item>
        ///             <description>
        ///                 Dictionary: <c>var dictParams = new Dictionary<string, object> { { "First", "A" } };</c>
        ///             </description>
        ///         </item>
        ///         <item>
        ///             <description>
        ///                 Array of objects: <c>var listParams = new Context[] { new() { First = "A" } };</c>
        ///             </description>
        ///         </item>
        ///     </list>
        /// </param>
        /// <param name="connection">An open connection to the data source.</param>
        /// <param name="transaction">The transaction to be used for the operation.</param>
        /// <returns>The number of affected records.</returns>
        Task<int> ExecuteAsync(string sql, object? param = null, IDbConnection? connection = null, IDbTransaction? transaction = null);
    }
}
