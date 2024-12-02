using System.Data;

namespace ITL.DataAccess
{
    /// <summary>
    /// Репозиторий для работы с SQL.
    /// </summary>
    public interface ISqlRepository
    {
        /// <summary>
        /// Получает запись.
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
        /// <typeparam name="T">Тип извлекаемой сущности.</typeparam>
        /// <returns>Сущность заданного типа T.</returns>
        T Query<T>(
            string sql, 
            object param = null, 
            IDbConnection connection = null, 
            IDbTransaction transaction = null);

        /// <summary>
        /// Получает список записей.
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
        /// <typeparam name="T">Тип извлекаемой сущности.</typeparam>
        /// <returns>Список сущностей заданного типа T.</returns>
        IEnumerable<T> QueryList<T>(
            string sql, 
            object param = null, 
            IDbConnection connection = null, 
            IDbTransaction transaction = null);

        /// <summary>
        /// Исполняет команду.
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
        /// <returns>Количество затронутых сущностей.</returns>
        int Execute(
            string sql, 
            object param = null, 
            IDbConnection connection = null, 
            IDbTransaction transaction = null);
    }
}
