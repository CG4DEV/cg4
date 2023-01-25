using System.Data;

namespace CG4.DataAccess
{
    /// <summary>
    /// CRUD-репозиторий.
    /// </summary>
    public interface ICrudRepository
    {
        /// <summary>
        /// Создает запись по переданному объекту.
        /// </summary>
        /// <param name="entity">Сущность, которую необходимо добавить.</param>
        /// <param name="connection">Открытое соединение с источником данных.</param>
        /// <param name="transaction">Транзакция, которая должна быть выполнена в источнике данных.</param>
        /// <typeparam name="T">Тип добавляемой сущности. Должен являться классом.</typeparam>
        /// <returns>Результат добавления сущности заданного типа T, определяющийся динамически.</returns>
        dynamic Insert<T>(T entity, IDbConnection connection = null, IDbTransaction transaction = null)
            where T : class;

        /// <summary>
        /// Обновляет запись по переданному объекту.
        /// </summary>
        /// <param name="entity">Сущность, которую необходимо обновить.</param>
        /// <param name="connection">Открытое соединение с источником данных.</param>
        /// <param name="transaction">Транзакция, которая должна быть выполнена в источнике данных.</param>
        /// <typeparam name="T">Тип обновляемой сущности. Должен являться классом.</typeparam>
        /// <returns>Результат успешности обновления сущности заданного типа T.</returns>
        bool Update<T>(T entity, IDbConnection connection = null, IDbTransaction transaction = null)
            where T : class;

        /// <summary>
        /// Удаляет запись по переданному объекту.
        /// </summary>
        /// <param name="entity">Сущность, которую необходимо удалить.</param>
        /// <param name="connection">Открытое соединение с источником данных.</param>
        /// <param name="transaction">Транзакция, которая должна быть выполнена в источнике данных.</param>
        /// <typeparam name="T">Тип удаляемой сущности. Должен являться классом.</typeparam>
        /// <returns>Результат успешности удаления сущности заданного типа T.</returns>
        bool Delete<T>(T entity, IDbConnection connection = null, IDbTransaction transaction = null)
            where T : class;
    }
}
