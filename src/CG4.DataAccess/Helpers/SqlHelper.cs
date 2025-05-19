using System.Data.SqlClient;

namespace CG4.DataAccess.Helpers
{
    /// <summary>
    /// Вспомогательный класс для работы с SQL.
    /// </summary>
    public static class SqlHelper
    {
        /// <summary>
        /// Преобразует объект параметров в массив SQL-параметров.
        /// </summary>
        /// <param name="param">Объект с параметрами.</param>
        /// <returns>Массив SQL-параметров.</returns>
        public static SqlParameter[] GetSqlParameter(object? param)
        {
            if (param == null)
            {
                return Array.Empty<SqlParameter>();
            }

            return param switch
            {
                Dictionary<string, object> dic => dic.Select(x => new SqlParameter(x.Key, x.Value)).ToArray(),
                _ => param.GetType().GetProperties().Select(field => new SqlParameter(field.Name, field.GetValue(param))).ToArray()
            };
        }
    }
}