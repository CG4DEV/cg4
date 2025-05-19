using System.Data;
using System.Data.Common;
using System.Data.SqlClient;

namespace CG4.DataAccess.Exceptions
{
    /// <summary>
    /// Фабрика для создания специализированных SQL-исключений.
    /// </summary>
    public static class SqlExceptionFactory
    {
        /// <summary>
        /// Создает подходящее исключение на основе исходного исключения.
        /// </summary>
        /// <param name="sql">SQL-запрос, вызвавший исключение.</param>
        /// <param name="parameters">Параметры SQL-запроса (если есть).</param>
        /// <param name="ex">Исходное исключение.</param>
        /// <param name="timeout">Значение таймаута в секундах (для ошибок таймаута).</param>
        /// <returns>Специализированное SQL-исключение.</returns>
        public static SqlExceptionBase Create(string sql, object? parameters, Exception ex, int? timeout = null)
        {
            // Проверка на тайм-аут
            if (ex is SqlException sqlEx && sqlEx.Number == -2)
            {
                return new SqlTimeoutException(sql, timeout ?? 0, parameters, ex);
            }

            // Обработка нарушений ограничений (уникальность, внешние ключи и т.д.)
            if (ex is DbException dbEx)
            {
                // Определение типа DB-ошибки на основе сообщения и/или специализированных свойств
                if (dbEx.Message.Contains("unique", StringComparison.OrdinalIgnoreCase) ||
                    dbEx.Message.Contains("duplicate", StringComparison.OrdinalIgnoreCase))
                {
                    return SqlDataException.UniqueConstraintViolation(sql, parameters, ex);
                }

                if (dbEx.Message.Contains("foreign key", StringComparison.OrdinalIgnoreCase) ||
                    dbEx.Message.Contains("reference", StringComparison.OrdinalIgnoreCase))
                {
                    return SqlDataException.ReferentialIntegrityViolation(sql, parameters, ex);
                }
            }

            // Обработка ошибок соединения
            if (ex is InvalidOperationException && ex.Message.Contains("connection", StringComparison.OrdinalIgnoreCase))
            {
                return new SqlConnectionException(sql, parameters, ex);
            }

            // Обработка ошибок транзакций
            if (ex is InvalidOperationException && 
                (ex.Message.Contains("transaction", StringComparison.OrdinalIgnoreCase) ||
                ex.Message.Contains("commit", StringComparison.OrdinalIgnoreCase) ||
                ex.Message.Contains("rollback", StringComparison.OrdinalIgnoreCase)))
            {
                return new SqlTransactionException(sql, parameters, ex);
            }

            // По умолчанию - общая ошибка запроса
            return new SqlQueryException(sql, parameters, ex);
        }

        /// <summary>
        /// Создает исключение для ошибки выполнения запроса.
        /// </summary>
        /// <param name="sql">SQL-запрос, вызвавший исключение.</param>
        /// <param name="parameters">Параметры SQL-запроса (если есть).</param>
        /// <param name="ex">Исходное исключение.</param>
        /// <returns>Исключение <see cref="SqlQueryException"/>.</returns>
        public static SqlQueryException CreateQueryException(string sql, object? parameters, Exception ex)
        {
            return new SqlQueryException(sql, parameters, ex);
        }

        /// <summary>
        /// Создает исключение для ошибки соединения.
        /// </summary>
        /// <param name="sql">SQL-запрос, вызвавший исключение.</param>
        /// <param name="parameters">Параметры SQL-запроса (если есть).</param>
        /// <param name="ex">Исходное исключение.</param>
        /// <returns>Исключение <see cref="SqlConnectionException"/>.</returns>
        public static SqlConnectionException CreateConnectionException(string sql, object? parameters, Exception ex)
        {
            return new SqlConnectionException(sql, parameters, ex);
        }

        /// <summary>
        /// Создает исключение для ошибки данных.
        /// </summary>
        /// <param name="sql">SQL-запрос, вызвавший исключение.</param>
        /// <param name="parameters">Параметры SQL-запроса (если есть).</param>
        /// <param name="ex">Исходное исключение.</param>
        /// <param name="message">Специальное сообщение об ошибке.</param>
        /// <returns>Исключение <see cref="SqlDataException"/>.</returns>
        public static SqlDataException CreateDataException(string sql, object? parameters, Exception ex, string? message = null)
        {
            return string.IsNullOrEmpty(message) 
                ? new SqlDataException(sql, parameters, ex) 
                : new SqlDataException(message, sql, parameters, ex);
        }
    }
} 