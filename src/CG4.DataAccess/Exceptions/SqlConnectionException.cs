namespace CG4.DataAccess.Exceptions
{
    /// <summary>
    /// Исключение, возникающее при ошибках подключения к базе данных.
    /// </summary>
    public class SqlConnectionException : SqlExceptionBase
    {
        /// <summary>
        /// Инициализирует новый экземпляр класса <see cref="SqlConnectionException"/>.
        /// </summary>
        /// <param name="message">Сообщение об ошибке.</param>
        /// <param name="sql">SQL-запрос, вызвавший исключение.</param>
        /// <param name="parameters">Параметры SQL-запроса (если есть).</param>
        /// <param name="innerException">Внутреннее исключение.</param>
        public SqlConnectionException(string message, string sql, object? parameters = null, Exception? innerException = null)
            : base(message, sql, parameters, innerException)
        {
        }

        /// <summary>
        /// Инициализирует новый экземпляр класса <see cref="SqlConnectionException"/> с сообщением по умолчанию.
        /// </summary>
        /// <param name="sql">SQL-запрос, вызвавший исключение.</param>
        /// <param name="parameters">Параметры SQL-запроса (если есть).</param>
        /// <param name="innerException">Внутреннее исключение.</param>
        public SqlConnectionException(string sql, object? parameters = null, Exception? innerException = null)
            : base("Ошибка соединения с базой данных", sql, parameters, innerException)
        {
        }
    }
} 