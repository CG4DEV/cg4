using System.Data;

namespace CG4.Impl.Dapper
{
    /// <summary>
    /// Interface for logging SQL queries.
    /// </summary>
    public interface ICrudServiceLogger
    {
        /// <summary>
        /// Logs the start of a query execution.
        /// </summary>
        /// <typeparam name="T">Type of the entity being queried.</typeparam>
        /// <param name="sql">SQL query.</param>
        void LogQueryStarted<T>(string sql);

        /// <summary>
        /// Logs the start of a query execution.
        /// </summary>
        /// <param name="sql">SQL query.</param>
        void LogQueryStarted(string sql);

        /// <summary>
        /// Logs query parameters.
        /// </summary>
        /// <param name="parameters">Query parameters.</param>
        void LogQueryParameters(object? parameters);
    }
} 