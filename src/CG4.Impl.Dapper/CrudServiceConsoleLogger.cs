namespace CG4.Impl.Dapper
{
    /// <summary>
    /// Implementation of ICrudServiceLogger that logs to the console.
    /// </summary>
    public class CrudServiceConsoleLogger : ICrudServiceLogger
    {
        /// <summary>
        /// Singleton instance of <see cref="CrudServiceConsoleLogger"/>.
        /// </summary>
        public static CrudServiceConsoleLogger Instance { get; } = new CrudServiceConsoleLogger();

        private CrudServiceConsoleLogger() { }

        /// <inheritdoc/>
        public void LogQueryStarted(string sql)
        {
            Console.WriteLine($"[SQL Query] SQL: {sql}");
        }

        /// <inheritdoc/>
        public void LogQueryStarted<T>(string sql)
        {
            Console.WriteLine($"[SQL Query] Type: {typeof(T).Name}, SQL: {sql}");
        }

        /// <inheritdoc/>
        public void LogQueryParameters(object? parameters)
        {
            if (parameters != null)
            {
                Console.WriteLine($"[SQL Params] {parameters}");
            }
        }
    }
} 