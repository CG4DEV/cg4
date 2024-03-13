using ITL.DataAccess.Poco.Visitors;

namespace ITL.DataAccess.Poco.Expressions
{
    public class ExprTableName : Expr
    {
        public string Alias { get; set; }

        public string TableName { get; set; }

        public override void Accept(IExprVisitor visitor) => visitor.VisitTableName(this);
    }
}
