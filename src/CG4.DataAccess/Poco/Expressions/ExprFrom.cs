using CG4.DataAccess.Poco.Visitors;

namespace CG4.DataAccess.Poco.Expressions
{
    public class ExprFrom : Expr
    {
        public ExprTableName TableName { get; set; }

        public ExprListJoins Joins { get; set; } = new ExprListJoins();

        public override void Accept(IExprVisitor visitor) => visitor.VisitFrom(this);
    }
}
