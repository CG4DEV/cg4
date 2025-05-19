namespace CG4.DataAccess.Exceptions
{
    /// <summary>
    /// Базовое исключение для всех SQL-связанных ошибок.
    /// </summary>
    public abstract class SqlExceptionBase : Exception
    {
        /// <summary>
        /// Получает SQL-запрос, вызвавший исключение.
        /// </summary>
        public string Sql { get; }

        /// <summary>
        /// Получает параметры SQL-запроса.
        /// </summary>
        public object? Parameters { get; }

        /// <summary>
        /// Инициализирует новый экземпляр класса <see cref="SqlExceptionBase"/>.
        /// </summary>
        /// <param name="message">Сообщение об ошибке.</param>
        /// <param name="sql">SQL-запрос, вызвавший исключение.</param>
        /// <param name="parameters">Параметры SQL-запроса (если есть).</param>
        /// <param name="innerException">Внутреннее исключение.</param>
        protected SqlExceptionBase(string message, string sql, object? parameters = null, Exception? innerException = null)
            : base(message, innerException)
        {
            Sql = sql;
            Parameters = parameters;
        }
    }
} 