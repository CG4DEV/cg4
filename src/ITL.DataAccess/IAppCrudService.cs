using System.Data;
using System.Linq.Expressions;
using ITL.DataAccess.Domain;
using ITL.DataAccess.Poco;

namespace ITL.DataAccess
{
    /// <summary>
    /// CRUD-сервис для работы с источником данных.
    /// </summary>
    public interface IAppCrudService
    {
        /// <summary>
        /// Асинхронно получает запись по уникальному идентификатору сущности.
        /// </summary>
        /// <param name="id">Уникальный идентификатор сущности.</param>
        /// <param name="connection">Открытое соединение с источником данных.</param>
        /// <param name="transaction">Транзакция, которая должна быть выполнена в источнике данных.</param>
        /// <param name="cancellationToken">Токен отмены операции.</param>
        /// <typeparam name="T">Тип извлекаемой сущности. Должен реализовывать <see cref="IEntityBase"/>.</typeparam>
        /// <returns>Сущность заданного типа T.</returns>
        Task<T> GetAsync<T>(
            long id, 
            IDbConnection connection = null, 
            IDbTransaction transaction = null,
            CancellationToken cancellationToken = default)
            where T : class, IEntityBase, new();

        /// <summary>
        /// Асинхронно получает запись по выражению.
        /// </summary>
        /// <param name="predicate">Выражение типа <see cref="IClassSqlOptions{TEntity}"/>.</param>
        /// <param name="connection">Открытое соединение с источником данных.</param>
        /// <param name="transaction">Транзакция, которая должна быть выполнена в источнике данных.</param>
        /// <param name="cancellationToken">Токен отмены операции.</param>
        /// <typeparam name="T">Тип извлекаемой сущности. Должен реализовывать <see cref="IEntityBase"/>.</typeparam>
        /// <returns>Сущность заданного типа T.</returns>
        Task<T> GetAsync<T>(
            Expression<Action<IClassSqlOptions<T>>> predicate, 
            IDbConnection connection = null, 
            IDbTransaction transaction = null,
            CancellationToken cancellationToken = default)
            where T : class, IEntityBase, new();

        /// <summary>
        /// Асинхронно получает запись по уникальному идентификатору сущности с преобразованием к другой сущности.
        /// </summary>
        /// <param name="id">Уникальный идентификатор сущности.</param>
        /// <param name="connection">Открытое соединение с источником данных.</param>
        /// <param name="transaction">Транзакция, которая должна быть выполнена в источнике данных.</param>
        /// <param name="cancellationToken">Токен отмены операции.</param>
        /// <typeparam name="TEntity">Тип извлекаемой сущности. Должен реализовывать <see cref="IEntityBase"/>.</typeparam>
        /// <typeparam name="TResult">Тип сущности, в которую необходимо преобразовать. Должен являться классом.</typeparam>
        /// <returns>Сущность заданного типа TResult.</returns>
        Task<TResult> GetAsync<TEntity, TResult>(
            long id, 
            IDbConnection connection = null, 
            IDbTransaction transaction = null, 
            CancellationToken cancellationToken = default)
            where TEntity : class, IEntityBase, new()
            where TResult : class, new();

        /// <summary>
        /// Асинхронно получает запись по выражению с преобразованием к другой сущности.
        /// </summary>
        /// <param name="predicate">Выражение типа <see cref="IClassSqlOptions{TEntity}"/>.</param>
        /// <param name="connection">Открытое соединение с источником данных.</param>
        /// <param name="transaction">Транзакция, которая должна быть выполнена в источнике данных.</param>
        /// <param name="cancellationToken">Токен отмены операции.</param>
        /// <typeparam name="TEntity">Тип извлекаемой сущности. Должен реализовывать <see cref="IEntityBase"/>.</typeparam>
        /// <typeparam name="TResult">Тип сущности, в которую необходимо преобразовать. Должен являться классом.</typeparam>
        /// <returns>Сущность заданного типа TResult.</returns>
        Task<TResult> GetAsync<TEntity, TResult>(
            Expression<Action<IClassSqlOptions<TEntity>>> predicate, 
            IDbConnection connection = null, 
            IDbTransaction transaction = null, 
            CancellationToken cancellationToken = default)
            where TEntity : class, IEntityBase, new()
            where TResult : class, new();

        /// <summary>
        /// Асинхронно получает список записей по выражению.
        /// </summary>
        /// <param name="predicate">Выражение типа <see cref="IClassSqlOptions{TEntity}"/>.</param>
        /// <param name="connection">Открытое соединение с источником данных.</param>
        /// <param name="transaction">Транзакция, которая должна быть выполнена в источнике данных.</param>
        /// <param name="cancellationToken">Токен отмены операции.</param>
        /// <typeparam name="T">Тип извлекаемой сущности. Должен реализовывать <see cref="IEntityBase"/>.</typeparam>
        /// <returns>Список сущностей заданного типа T.</returns>
        Task<IEnumerable<T>> GetAllAsync<T>(
            Expression<Action<IClassSqlOptions<T>>> predicate = null, 
            IDbConnection connection = null, 
            IDbTransaction transaction = null, 
            CancellationToken cancellationToken = default)
            where T : class, IEntityBase, new();

        /// <summary>
        /// Асинхронно получает список записей по выражению с преобразованием к другой сущности.
        /// </summary>
        /// <param name="predicate">Выражение типа <see cref="IClassSqlOptions{TEntity}"/>.</param>
        /// <param name="connection">Открытое соединение с источником данных.</param>
        /// <param name="transaction">Транзакция, которая должна быть выполнена в источнике данных.</param>
        /// <param name="cancellationToken">Токен отмены операции.</param>
        /// <typeparam name="TEntity">Тип извлекаемой сущности. Должен реализовывать <see cref="IEntityBase"/>.</typeparam>
        /// <typeparam name="TResult">Тип сущности, в которую необходимо преобразовать. Должен являться классом.</typeparam>
        /// <returns>Список сущностей заданного типа TResult.</returns>
        Task<IEnumerable<TResult>> GetAllAsync<TEntity, TResult>(
            Expression<Action<IClassSqlOptions<TEntity>>> predicate = null, 
            IDbConnection connection = null, 
            IDbTransaction transaction = null, 
            CancellationToken cancellationToken = default)
            where TEntity : class, IEntityBase, new()
            where TResult : class, new();

        /// <summary>
        /// Асинхронно создает запись по переданному объекту.
        /// </summary>
        /// <param name="entity">Сущность, которую необходимо добавить.</param>
        /// <param name="connection">Открытое соединение с источником данных.</param>
        /// <param name="transaction">Транзакция, которая должна быть выполнена в источнике данных.</param>
        /// <param name="cancellationToken">Токен отмены операции.</param>
        /// <typeparam name="T">Тип добавляемой сущности. Должен реализовывать <see cref="IEntityBase"/>.</typeparam>
        /// <returns>Созданная сущность заданного типа T.</returns>
        Task<T> CreateAsync<T>(
            T entity, 
            IDbConnection connection = null, 
            IDbTransaction transaction = null, 
            CancellationToken cancellationToken = default)
            where T : class, IEntityBase, new();

        /// <summary>
        /// Асинхронно обновляет запись по переданному объекту.
        /// </summary>
        /// <param name="entity">Сущность, которую необходимо обновить.</param>
        /// <param name="connection">Открытое соединение с источником данных.</param>
        /// <param name="transaction">Транзакция, которая должна быть выполнена в источнике данных.</param>
        /// <param name="cancellationToken">Токен отмены операции.</param>
        /// <typeparam name="T">Тип обновляемой сущности. Должен реализовывать <see cref="IEntityBase"/>.</typeparam>
        /// <returns>Обновленная сущность заданного типа T.</returns>
        Task<T> UpdateAsync<T>(
            T entity, 
            IDbConnection connection = null, 
            IDbTransaction transaction = null, 
            CancellationToken cancellationToken = default)
            where T : class, IEntityBase, new();

        /// <summary>
        /// Асинхронно удаляет запись по переданному объекту.
        /// </summary>
        /// <param name="id">Уникальный идентификатор сущности.</param>
        /// <param name="connection">Открытое соединение с источником данных.</param>
        /// <param name="transaction">Транзакция, которая должна быть выполнена в источнике данных.</param>
        /// <param name="cancellationToken">Токен отмены операции.</param>
        /// <typeparam name="T">Тип обновляемой сущности. Должен реализовывать <see cref="IEntityBase"/>.</typeparam>
        Task DeleteAsync<T>(
            long id, 
            IDbConnection connection = null, 
            IDbTransaction transaction = null, 
            CancellationToken cancellationToken = default)
            where T : class, IEntityBase, new();

        /// <summary>
        /// Асинхронно получает постраничный вывод записей по выражению.
        /// </summary>
        /// <param name="page">Номер страницы.</param>
        /// <param name="take">Количество сущностей для изъятия.</param>
        /// <param name="predicate">Выражение типа <see cref="IClassSqlOptions{TEntity}"/>.</param>
        /// <param name="connection">Открытое соединение с источником данных.</param>
        /// <param name="transaction">Транзакция, которая должна быть выполнена в источнике данных.</param>
        /// <param name="cancellationToken">Токен отмены операции.</param>
        /// <typeparam name="TEntity">Тип извлекаемой сущности. Должен реализовывать <see cref="IEntityBase"/>.</typeparam>
        /// <returns>Страница сущностей заданного типа TEntity.</returns>
        Task<PageResult<TEntity>> GetPageAsync<TEntity>(
            int? page, 
            int? take, 
            Expression<Action<IClassSqlOptions<TEntity>>> predicate = null, 
            IDbConnection connection = null, 
            IDbTransaction transaction = null, 
            CancellationToken cancellationToken = default)
            where TEntity : class, IEntityBase, new();

        /// <summary>
        /// Асинхронно получает постраничный вывод записей с преобразованием к другой сущности по выражению.
        /// </summary>
        /// <param name="page">Номер страницы.</param>
        /// <param name="take">Количество сущностей для изъятия.</param>
        /// <param name="predicate">Выражение типа <see cref="IClassSqlOptions{TEntity}"/>.</param>
        /// <param name="connection">Открытое соединение с источником данных.</param>
        /// <param name="transaction">Транзакция, которая должна быть выполнена в источнике данных.</param>
        /// <param name="cancellationToken">Токен отмены операции.</param>
        /// <typeparam name="TEntity">Тип извлекаемой сущности. Должен реализовывать <see cref="IEntityBase"/>.</typeparam>
        /// <typeparam name="TResult">Тип сущности, в которую необходимо преобразовать. Должен являться классом.</typeparam>
        /// <returns>Страница сущностей заданного типа TResult.</returns>
        Task<PageResult<TResult>> GetPageAsync<TEntity, TResult>(
            int? page, 
            int? take, 
            Expression<Action<IClassSqlOptions<TEntity>>> predicate = null, 
            IDbConnection connection = null, 
            IDbTransaction transaction = null, 
            CancellationToken cancellationToken = default)
            where TEntity : class, IEntityBase, new()
            where TResult : class, new();
    }
}
