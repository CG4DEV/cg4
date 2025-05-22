using CG4.DataAccess;
using Microsoft.Extensions.Logging;

namespace CG4.Impl.Dapper
{
    /// <summary>
    /// Implementation of ICrudServiceLogger using Microsoft.Extensions.Logging.
    /// </summary>
    public class CrudServiceLogger : ICrudServiceLogger
    {
        private readonly ILogger _logger;

        /// <summary>
        /// Initializes a new instance of <see cref="CrudServiceLogger"/>.
        /// </summary>
        /// <param name="logger">Logger.</param>
        public CrudServiceLogger(ILogger<ICrudService> logger)
        {
            _logger = logger;
        }

        /// <inheritdoc/>
        public void LogQueryStarted(string sql)
        {
            _logger.LogDebug("[SQL Query] SQL: {Sql}", sql);
        }

        /// <inheritdoc/>
        public void LogQueryStarted<T>(string sql)
        {
            _logger.LogDebug("[SQL Query] Type: {EntityType}, SQL: {Sql}", typeof(T).Name, sql);
        }

        /// <inheritdoc/>
        public void LogQueryParameters(object? parameters)
        {
            if (parameters != null)
            {
                _logger.LogDebug("[SQL Parameters]: {@Params}", parameters);
            }
        }
    }
} 