using ITL.DataAccess.Poco.Expressions;

namespace ITL.Impl.Dapper.Poco.ExprOptions
{
    public class ExprSqlOptionsResult
    {
        public static ExprSqlOptionsResult Default { get; } = new ExprSqlOptionsResult();
        public ExprSql Sql { get; set; }
    }
}
