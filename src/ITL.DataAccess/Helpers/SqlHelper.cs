using System.Data.SqlClient;

namespace ITL.DataAccess.Helpers
{
    public static class SqlHelper
    {
        public static SqlParameter[] GetSqlParameter(object param)
        {
            if (param != null)
                switch (param)
                {
                    case Dictionary<string, object> dic:
                        return dic.Select(x => new SqlParameter(x.Key, x.Value)).ToArray();
                    case object o:
                        return param.GetType().GetProperties().Select(field => new SqlParameter(field.Name, field.GetValue(param))).ToArray();
                }

            return Array.Empty<SqlParameter>();
        }
    }
}