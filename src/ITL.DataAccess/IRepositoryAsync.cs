using System.Data;
using System.Linq.Expressions;

namespace ITL.DataAccess
{
    /// <summary>
    /// Асинхронный репозиторий.
    /// </summary>
    public interface IRepositoryAsync
    {
        /// <summary>
        /// Асинхронно получает запись по выражению.
        /// </summary>
        /// <param name="predicate">Выражение, принимающее функцию для поиска сущности.</param>
        /// <param name="connection">Открытое соединение с источником данных.</param>
        /// <param name="transaction">Транзакция, которая должна быть выполнена в источнике данных.</param>
        /// <typeparam name="T">Тип извлекаемой сущности. Должен являться классом.</typeparam>
        /// <returns>Сущность заданного типа T.</returns>
        Task<T> GetAsync<T>(Expression<Func<T, bool>> predicate, IDbConnection connection = null, IDbTransaction transaction = null)
            where T : class;

        /// <summary>
        /// Асинхронно получает список записей по выражению.
        /// </summary>
        /// <param name="predicate">Выражение, принимающее функцию для поиска сущности.</param>
        /// <param name="connection">Открытое соединение с источником данных.</param>
        /// <param name="transaction">Транзакция, которая должна быть выполнена в источнике данных.</param>
        /// <typeparam name="T">Тип извлекаемой сущности. Должен являться классом.</typeparam>
        /// <returns>Список сущностей заданного типа T.</returns>
        Task<IEnumerable<T>> GetAllAsync<T>(Expression<Func<T, bool>> predicate = null, IDbConnection connection = null, IDbTransaction transaction = null)
            where T : class;

        /// <summary>
        /// Асинхронно получает список записей по выражению с возможностью сортировки с преобразованием к другой сущности.
        /// </summary>
        /// <param name="predicate">Выражение, принимающее функцию для поиска сущности.</param>
        /// <param name="orderSelector">Функция, задающая настройки сортировки.</param>
        /// <param name="isAcending">Флаг, задающий сортировку по возрастанию.</param>
        /// <param name="connection">Открытое соединение с источником данных.</param>
        /// <param name="transaction">Транзакция, которая должна быть выполнена в источнике данных.</param>
        /// <typeparam name="T">Тип извлекаемой сущности. Должен являться классом.</typeparam>
        /// <typeparam name="TKey">Тип сущности, в которую необходимо преобразовать.</typeparam>
        /// <returns>Список сущностей заданного типа TKey.</returns>
        Task<IEnumerable<T>> GetAllAsync<T, TKey>(Expression<Func<T, bool>> predicate = null, Func<T, TKey> orderSelector = null, bool isAcending = true, IDbConnection connection = null, IDbTransaction transaction = null)
            where T : class;

        /// <summary>
        /// Асинхронно получает постраничный вывод записей по выражению.
        /// </summary>
        /// <param name="predicate">Выражение, принимающее функцию для поиска сущности.</param>
        /// <param name="page">Номер страницы.</param>
        /// <param name="resultsPerPage">Количество сущностей для изъятия.</param>
        /// <typeparam name="T">Тип извлекаемой сущности. Должен являться классом.</typeparam>
        /// <returns>Список сущностей заданного типа T.</returns>
        Task<IEnumerable<T>> GetPageAsync<T>(Expression<Func<T, bool>> predicate = null, int page = 0, int resultsPerPage = 10)
            where T : class;

        /// <summary>
        /// Асинхронно получает постраничный вывод записей по выражению с преобразованием к другой сущности.
        /// </summary>
        /// <param name="predicate">Выражение, принимающее функцию для поиска сущности.</param>
        /// <param name="orderSelector">Функция, задающая настройки сортировки.</param>
        /// <param name="isAscending">Флаг, задающий сортировку по возрастанию.</param>
        /// <param name="page">Номер страницы.</param>
        /// <param name="resultsPerPage">Количество сущностей для изъятия.</param>
        /// <typeparam name="T">Тип извлекаемой сущности. Должен являться классом.</typeparam>
        /// <typeparam name="TKey">Тип сущности, в которую необходимо преобразовать.</typeparam>
        /// <returns>Список сущностей заданного типа TKey.</returns>
        Task<IEnumerable<T>> GetPageAsync<T, TKey>(Expression<Func<T, bool>> predicate = null, Func<T, TKey> orderSelector = null, bool isAscending = true, int page = 0, int resultsPerPage = 10)
            where T : class;
    }
}
