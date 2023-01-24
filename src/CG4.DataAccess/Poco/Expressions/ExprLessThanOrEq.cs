using CG4.DataAccess.Poco.Visitors;

namespace CG4.DataAccess.Poco.Expressions
{
    public class ExprLessThanOrEq : ExprBinary
    {
        public override void Accept(IExprVisitor visitor) => visitor.VisitLessThanOrEqPredicate(this);
    }
}
