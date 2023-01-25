using CG4.DataAccess.Poco.Expressions;

namespace CG4.Impl.Dapper.Poco.ExprOptions
{
    public class ExprSqlOptionsResult
    {
        public static ExprSqlOptionsResult Default { get; } = new ExprSqlOptionsResult();
        public ExprSql Sql { get; set; }
    }
}
