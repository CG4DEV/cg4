namespace CG4.Impl.Dapper
{
    /// <summary>
    /// Implementation of ICrudServiceLogger that does not perform any logging.
    /// </summary>
    public class CrudServiceNullLogger : ICrudServiceLogger
    {
        /// <summary>
        /// Singleton instance of <see cref="CrudServiceNullLogger"/>.
        /// </summary>
        public static CrudServiceNullLogger Instance { get; } = new CrudServiceNullLogger();

        private CrudServiceNullLogger() { }

        /// <inheritdoc/>
        public void LogQueryStarted<T>(string sql) { }

        /// <inheritdoc/>
        public void LogQueryStarted(string sql) { }

        /// <inheritdoc/>
        public void LogQueryParameters(object? parameters) { }
    }
} 