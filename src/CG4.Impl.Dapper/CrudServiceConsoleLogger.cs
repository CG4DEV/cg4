namespace CG4.Impl.Dapper
{
    /// <summary>
    /// Implementation of ICrudServiceLogger that logs to the console.
    /// </summary>
    public class CrudServiceConsoleLogger : ICrudServiceLogger
    {
        /// <inheritdoc/>
        public void LogQueryStarted(string sql)
        {
            Console.ForegroundColor = ConsoleColor.DarkCyan;
            Console.WriteLine($"[SQL Query] SQL:");
            Console.ResetColor();
            Console.WriteLine(sql);
        }

        /// <inheritdoc/>
        public void LogQueryStarted<T>(string sql)
        {
            Console.ForegroundColor = ConsoleColor.DarkCyan;
            Console.WriteLine($"[SQL Query] Type: {typeof(T).Name}, SQL:");
            Console.ResetColor();
            Console.WriteLine(sql);
        }

        /// <inheritdoc/>
        public void LogQueryParameters(object? parameters)
        {
            if (parameters != null)
            {
                Console.ForegroundColor = ConsoleColor.Blue;
                Console.WriteLine($"[SQL Params] {parameters}");
            }
        }
    }
} 