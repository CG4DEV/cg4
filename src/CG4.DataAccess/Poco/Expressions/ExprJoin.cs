using CG4.DataAccess.Poco.Visitors;

namespace CG4.DataAccess.Poco.Expressions
{
    public class ExprJoin : Expr
    {
        public ExprTableName TableName { get; set; }

        public ExprColumn TableColumn { get; set; }

        public ExprColumn OtherColumn { get; set; }

        public override void Accept(IExprVisitor visitor) => visitor.VisitJoin(this);
    }
}
