using ITL.DataAccess.Poco.Visitors;

namespace ITL.DataAccess.Poco.Expressions
{
    public class ExprStar : Expr
    {
        public override void Accept(IExprVisitor visitor) => visitor.VisitStar(this);
    }
}
