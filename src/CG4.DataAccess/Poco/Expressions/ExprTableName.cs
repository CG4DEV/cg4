using CG4.DataAccess.Poco.Visitors;

namespace CG4.DataAccess.Poco.Expressions
{
    public class ExprTableName : Expr
    {
        public string Alias { get; set; }

        public string TableName { get; set; }

        public override void Accept(IExprVisitor visitor) => visitor.VisitTableName(this);
    }
}
