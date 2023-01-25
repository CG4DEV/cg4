using System.Data;

namespace CG4.DataAccess
{
    /// <summary>
    /// Фабрика, создающая подключение к источнику данных.
    /// </summary>
    public interface IConnectionFactory
    {
        /// <summary>
        /// Создаёт подключение к базе данных.
        /// </summary>
        /// <returns>Объект подключения.</returns>
        IDbConnection Create();

        /// <summary>
        /// Создаёт подключение к базе данных по переданной строке подключения.
        /// </summary>
        /// <param name="connectionString">Строка подключения к источнику данных.</param>
        /// <returns>Объект подключения.</returns>
        IDbConnection Create(string connectionString);

        /// <summary>
        /// Асинхронно создаёт подключение к базе данных.
        /// </summary>
        /// <returns>Объект подключения.</returns>
        Task<IDbConnection> CreateAsync();

        /// <summary>
        /// Асинхронно создаёт подключение к базе данных по переданной строке подключения.
        /// </summary>
        /// <param name="connectionString">Строка подключения к источнику данных.</param>
        /// <returns>Объект подключения.</returns>
        Task<IDbConnection> CreateAsync(string connectionString);
    }
}
