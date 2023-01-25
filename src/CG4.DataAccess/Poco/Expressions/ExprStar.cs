using CG4.DataAccess.Poco.Visitors;

namespace CG4.DataAccess.Poco.Expressions
{
    public class ExprStar : Expr
    {
        public override void Accept(IExprVisitor visitor) => visitor.VisitStar(this);
    }
}
