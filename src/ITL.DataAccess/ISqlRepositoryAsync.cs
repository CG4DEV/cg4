using System.Data;

namespace ITL.DataAccess
{
    public interface ISqlRepositoryAsync
    {
        /// <summary>
        /// Асинхронно получает запись.
        /// </summary>
        /// <param name="sql">SQL-строка запроса.</param>
        /// <param name="param">
        ///     <list type="number">
        ///         <item>
        ///             <description>
        ///                 <c>var objectParams = new { First = "A" };</c>
        ///             </description>
        ///         </item>
        ///         <item>
        ///             <description>
        ///                 <c>var objectParams = new Context { First = "A" };</c>
        ///             </description>
        ///         </item>
        ///         <item>
        ///             <description>
        ///                 <c>
        ///                     var dictParams = new Dictionary&#60;string, object&#62;
        ///                     {
        ///                         { "First", "A" }
        ///                     };
        ///                 </c>
        ///             </description>
        ///         </item>
        ///         <item>
        ///             <description>
        ///                 <c>var listParams = new Context[] { new() { First = "A" } };</c>
        ///             </description>
        ///         </item>
        ///     </list>
        /// </param>
        /// <param name="connection">Открытое соединение с источником данных.</param>
        /// <param name="transaction">Транзакция, которая должна быть выполнена в источнике данных.</param>
        /// <param name="cancellationToken">Токен отмены операции.</param>
        /// <typeparam name="T">Тип извлекаемой сущности.</typeparam>
        /// <returns>Сущность заданного типа T.</returns>
        Task<T> QueryAsync<T>(
            string sql, 
            object param = null, 
            IDbConnection connection = null, 
            IDbTransaction transaction = null, 
            CancellationToken cancellationToken = default);

        /// <summary>
        /// Асинхронно получает список записей.
        /// </summary>
        /// <param name="sql">SQL-строка запроса.</param>
        /// <param name="param">
        ///     <list type="number">
        ///         <item>
        ///             <description>
        ///                 <c>var objectParams = new { First = "A" };</c>
        ///             </description>
        ///         </item>
        ///         <item>
        ///             <description>
        ///                 <c>var objectParams = new Context { First = "A" };</c>
        ///             </description>
        ///         </item>
        ///         <item>
        ///             <description>
        ///                 <c>
        ///                     var dictParams = new Dictionary&#60;string, object&#62;
        ///                     {
        ///                         { "First", "A" }
        ///                     };
        ///                 </c>
        ///             </description>
        ///         </item>
        ///         <item>
        ///             <description>
        ///                 <c>var listParams = new Context[] { new() { First = "A" } };</c>
        ///             </description>
        ///         </item>
        ///     </list>
        /// </param>
        /// <param name="connection">Открытое соединение с источником данных.</param>
        /// <param name="transaction">Транзакция, которая должна быть выполнена в источнике данных.</param>
        /// <param name="cancellationToken">Токен отмены операции.</param>
        /// <typeparam name="T">Тип извлекаемой сущности.</typeparam>
        /// <returns>Список сущностей заданного типа T.</returns>
        Task<IEnumerable<T>> QueryListAsync<T>(
            string sql, 
            object param = null, 
            IDbConnection connection = null, 
            IDbTransaction transaction = null,
            CancellationToken cancellationToken = default);

        /// <summary>
        /// Асинхронно исполняет команду.
        /// </summary>
        /// <param name="sql">SQL-строка запроса.</param>
        /// <param name="param">
        ///     <list type="number">
        ///         <item>
        ///             <description>
        ///                 <c>var objectParams = new { First = "A" };</c>
        ///             </description>
        ///         </item>
        ///         <item>
        ///             <description>
        ///                 <c>var objectParams = new Context { First = "A" };</c>
        ///             </description>
        ///         </item>
        ///         <item>
        ///             <description>
        ///                 <c>
        ///                     var dictParams = new Dictionary&#60;string, object&#62;
        ///                     {
        ///                         { "First", "A" }
        ///                     };
        ///                 </c>
        ///             </description>
        ///         </item>
        ///         <item>
        ///             <description>
        ///                 <c>var listParams = new Context[] { new() { First = "A" } };</c>
        ///             </description>
        ///         </item>
        ///     </list>
        /// </param>
        /// <param name="connection">Открытое соединение с источником данных.</param>
        /// <param name="transaction">Транзакция, которая должна быть выполнена в источнике данных.</param>
        /// <param name="cancellationToken">Токен отмены операции.</param>
        /// <returns>Количество затронутых сущностей.</returns>
        Task<int> ExecuteAsync(
            string sql, 
            object param = null, 
            IDbConnection connection = null, 
            IDbTransaction transaction = null, 
            CancellationToken cancellationToken = default);
    }
}
