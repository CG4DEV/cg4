namespace CG4.DataAccess.Exceptions
{
    /// <summary>
    /// Исключение, возникающее при превышении времени выполнения SQL-запроса.
    /// </summary>
    public class SqlTimeoutException : SqlExceptionBase
    {
        /// <summary>
        /// Получает значение таймаута в секундах.
        /// </summary>
        public int Timeout { get; }

        /// <summary>
        /// Инициализирует новый экземпляр класса <see cref="SqlTimeoutException"/>.
        /// </summary>
        /// <param name="message">Сообщение об ошибке.</param>
        /// <param name="sql">SQL-запрос, вызвавший исключение.</param>
        /// <param name="timeout">Значение таймаута в секундах.</param>
        /// <param name="parameters">Параметры SQL-запроса (если есть).</param>
        /// <param name="innerException">Внутреннее исключение.</param>
        public SqlTimeoutException(string message, string sql, int timeout, object? parameters = null, Exception? innerException = null)
            : base(message, sql, parameters, innerException)
        {
            Timeout = timeout;
        }

        /// <summary>
        /// Инициализирует новый экземпляр класса <see cref="SqlTimeoutException"/> с сообщением по умолчанию.
        /// </summary>
        /// <param name="sql">SQL-запрос, вызвавший исключение.</param>
        /// <param name="timeout">Значение таймаута в секундах.</param>
        /// <param name="parameters">Параметры SQL-запроса (если есть).</param>
        /// <param name="innerException">Внутреннее исключение.</param>
        public SqlTimeoutException(string sql, int timeout, object? parameters = null, Exception? innerException = null)
            : base($"Превышено время выполнения SQL-запроса (таймаут: {timeout} сек.)", sql, parameters, innerException)
        {
            Timeout = timeout;
        }
    }
} 