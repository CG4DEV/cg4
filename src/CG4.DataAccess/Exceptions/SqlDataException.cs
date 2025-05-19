namespace CG4.DataAccess.Exceptions
{
    /// <summary>
    /// Исключение, возникающее при ошибках с данными в SQL-запросах.
    /// </summary>
    public class SqlDataException : SqlExceptionBase
    {
        /// <summary>
        /// Инициализирует новый экземпляр класса <see cref="SqlDataException"/>.
        /// </summary>
        /// <param name="message">Сообщение об ошибке.</param>
        /// <param name="sql">SQL-запрос, вызвавший исключение.</param>
        /// <param name="parameters">Параметры SQL-запроса (если есть).</param>
        /// <param name="innerException">Внутреннее исключение.</param>
        public SqlDataException(string message, string sql, object? parameters = null, Exception? innerException = null)
            : base(message, sql, parameters, innerException)
        {
        }

        /// <summary>
        /// Инициализирует новый экземпляр класса <see cref="SqlDataException"/> с сообщением по умолчанию.
        /// </summary>
        /// <param name="sql">SQL-запрос, вызвавший исключение.</param>
        /// <param name="parameters">Параметры SQL-запроса (если есть).</param>
        /// <param name="innerException">Внутреннее исключение.</param>
        public SqlDataException(string sql, object? parameters = null, Exception? innerException = null)
            : base("Ошибка данных при выполнении SQL-запроса", sql, parameters, innerException)
        {
        }

        /// <summary>
        /// Создает исключение для случая, когда не найдена запись.
        /// </summary>
        /// <param name="sql">SQL-запрос, вызвавший исключение.</param>
        /// <param name="parameters">Параметры SQL-запроса (если есть).</param>
        /// <returns>Экземпляр <see cref="SqlDataException"/>.</returns>
        public static SqlDataException RecordNotFound(string sql, object? parameters = null)
        {
            return new SqlDataException("Запись не найдена", sql, parameters);
        }

        /// <summary>
        /// Создает исключение для случая, когда нарушена уникальность данных.
        /// </summary>
        /// <param name="sql">SQL-запрос, вызвавший исключение.</param>
        /// <param name="parameters">Параметры SQL-запроса (если есть).</param>
        /// <param name="innerException">Внутреннее исключение.</param>
        /// <returns>Экземпляр <see cref="SqlDataException"/>.</returns>
        public static SqlDataException UniqueConstraintViolation(string sql, object? parameters = null, Exception? innerException = null)
        {
            return new SqlDataException("Нарушено ограничение уникальности", sql, parameters, innerException);
        }

        /// <summary>
        /// Создает исключение для случая, когда нарушена целостность данных.
        /// </summary>
        /// <param name="sql">SQL-запрос, вызвавший исключение.</param>
        /// <param name="parameters">Параметры SQL-запроса (если есть).</param>
        /// <param name="innerException">Внутреннее исключение.</param>
        /// <returns>Экземпляр <see cref="SqlDataException"/>.</returns>
        public static SqlDataException ReferentialIntegrityViolation(string sql, object? parameters = null, Exception? innerException = null)
        {
            return new SqlDataException("Нарушена целостность данных (ограничение внешнего ключа)", sql, parameters, innerException);
        }
    }
} 