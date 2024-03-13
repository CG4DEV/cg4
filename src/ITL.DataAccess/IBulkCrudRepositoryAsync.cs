using System.Data;

namespace ITL.DataAccess
{
    /// <summary>
    /// Объемный асинхронный CRUD-репозиторий.
    /// </summary>
    public interface IBulkCrudRepositoryAsync
    {
        /// <summary>
        /// Асинхронно удаляет список записей по переданным объектам.
        /// </summary>
        /// <param name="entities">Сущности, которые необходимо удалить.</param>
        /// <param name="connection">Открытое соединение с источником данных.</param>
        /// <param name="transaction">Транзакция, которая должна быть выполнена в источнике данных.</param>
        /// <typeparam name="T">Тип удаляемой сущности. Должен являться классом.</typeparam>
        /// <returns>Список удаленных сущностей заданного типа T.</returns>
        public Task<IEnumerable<T>> DeleteAsync<T>(IEnumerable<T> entities, IDbConnection connection = null, IDbTransaction transaction = null)
            where T : class;

        /// <summary>
        /// Асинхронно добавляет список записей по переданным объектам.
        /// </summary>
        /// <param name="entities">Сущности, которые необходимо добавить.</param>
        /// <param name="connection">Открытое соединение с источником данных.</param>
        /// <param name="transaction">Транзакция, которая должна быть выполнена в источнике данных.</param>
        /// <typeparam name="T">Тип добавляемой сущности. Должен являться классом.</typeparam>
        /// <returns>Список добавленных сущностей заданного типа T.</returns>
        public Task<IEnumerable<T>> InsertAsync<T>(IEnumerable<T> entities, IDbConnection connection = null, IDbTransaction transaction = null)
            where T : class;

        /// <summary>
        /// Асинхронно обновляет список записей по переданным объектам.
        /// </summary>
        /// <param name="entities">Сущности, которые необходимо обновить.</param>
        /// <param name="connection">Открытое соединение с источником данных.</param>
        /// <param name="transaction">Транзакция, которая должна быть выполнена в источнике данных.</param>
        /// <typeparam name="T">Тип обновляемой сущности. Должен являться классом.</typeparam>
        /// <returns>Список обновленных сущностей заданного типа T.</returns>
        public Task<IEnumerable<T>> UpdateAsync<T>(IEnumerable<T> entities, IDbConnection connection = null, IDbTransaction transaction = null)
            where T : class;
    }
}
