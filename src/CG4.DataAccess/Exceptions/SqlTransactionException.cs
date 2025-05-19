namespace CG4.DataAccess.Exceptions
{
    /// <summary>
    /// Исключение, возникающее при ошибках транзакций в базе данных.
    /// </summary>
    public class SqlTransactionException : SqlExceptionBase
    {
        /// <summary>
        /// Инициализирует новый экземпляр класса <see cref="SqlTransactionException"/>.
        /// </summary>
        /// <param name="message">Сообщение об ошибке.</param>
        /// <param name="sql">SQL-запрос, вызвавший исключение.</param>
        /// <param name="parameters">Параметры SQL-запроса (если есть).</param>
        /// <param name="innerException">Внутреннее исключение.</param>
        public SqlTransactionException(string message, string sql, object? parameters = null, Exception? innerException = null)
            : base(message, sql, parameters, innerException)
        {
        }

        /// <summary>
        /// Инициализирует новый экземпляр класса <see cref="SqlTransactionException"/> с сообщением по умолчанию.
        /// </summary>
        /// <param name="sql">SQL-запрос, вызвавший исключение.</param>
        /// <param name="parameters">Параметры SQL-запроса (если есть).</param>
        /// <param name="innerException">Внутреннее исключение.</param>
        public SqlTransactionException(string sql, object? parameters = null, Exception? innerException = null)
            : base("Ошибка транзакции в базе данных", sql, parameters, innerException)
        {
        }

        /// <summary>
        /// Создает исключение для ошибки начала транзакции.
        /// </summary>
        /// <param name="innerException">Внутреннее исключение.</param>
        /// <returns>Экземпляр <see cref="SqlTransactionException"/>.</returns>
        public static SqlTransactionException BeginFailed(Exception innerException)
        {
            return new SqlTransactionException("Не удалось начать транзакцию", string.Empty, null, innerException);
        }

        /// <summary>
        /// Создает исключение для ошибки фиксации транзакции.
        /// </summary>
        /// <param name="innerException">Внутреннее исключение.</param>
        /// <returns>Экземпляр <see cref="SqlTransactionException"/>.</returns>
        public static SqlTransactionException CommitFailed(Exception innerException)
        {
            return new SqlTransactionException("Не удалось зафиксировать транзакцию", string.Empty, null, innerException);
        }

        /// <summary>
        /// Создает исключение для ошибки отката транзакции.
        /// </summary>
        /// <param name="innerException">Внутреннее исключение.</param>
        /// <returns>Экземпляр <see cref="SqlTransactionException"/>.</returns>
        public static SqlTransactionException RollbackFailed(Exception innerException)
        {
            return new SqlTransactionException("Не удалось выполнить откат транзакции", string.Empty, null, innerException);
        }
    }
} 