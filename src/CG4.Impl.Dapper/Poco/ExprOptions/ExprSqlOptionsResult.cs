using CG4.Impl.Dapper.Poco.Expressions;

namespace CG4.Impl.Dapper.Poco.ExprOptions
{
    public class ExprSqlOptionsResult
    {
        public static ExprSqlOptionsResult Default { get; } = new ExprSqlOptionsResult();
        public ExprSql Sql { get; set; }
    }
}
