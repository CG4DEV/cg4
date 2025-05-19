namespace CG4.DataAccess.Exceptions
{
    /// <summary>
    /// Исключение, возникающее при ошибках выполнения SQL-запросов.
    /// </summary>
    public class SqlQueryException : SqlExceptionBase
    {
        /// <summary>
        /// Инициализирует новый экземпляр класса <see cref="SqlQueryException"/>.
        /// </summary>
        /// <param name="message">Сообщение об ошибке.</param>
        /// <param name="sql">SQL-запрос, вызвавший исключение.</param>
        /// <param name="parameters">Параметры SQL-запроса (если есть).</param>
        /// <param name="innerException">Внутреннее исключение.</param>
        public SqlQueryException(string message, string sql, object? parameters = null, Exception? innerException = null)
            : base(message, sql, parameters, innerException)
        {
        }

        /// <summary>
        /// Инициализирует новый экземпляр класса <see cref="SqlQueryException"/> с сообщением по умолчанию.
        /// </summary>
        /// <param name="sql">SQL-запрос, вызвавший исключение.</param>
        /// <param name="parameters">Параметры SQL-запроса (если есть).</param>
        /// <param name="innerException">Внутреннее исключение.</param>
        public SqlQueryException(string sql, object? parameters = null, Exception? innerException = null)
            : base("Ошибка выполнения SQL-запроса", sql, parameters, innerException)
        {
        }
    }
} 