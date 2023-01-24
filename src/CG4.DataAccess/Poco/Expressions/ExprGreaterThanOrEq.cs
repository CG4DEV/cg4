using CG4.DataAccess.Poco.Visitors;

namespace CG4.DataAccess.Poco.Expressions
{
    public class ExprGreaterThanOrEq : ExprBinary
    {
        public override void Accept(IExprVisitor visitor) => visitor.VisitGreaterThanOrEqPredicate(this);
    }
}
