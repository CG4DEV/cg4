using ITL.DataAccess.Poco.Visitors;

namespace ITL.DataAccess.Poco.Expressions
{
    public class ExprGreaterThanOrEq : ExprBinary
    {
        public override void Accept(IExprVisitor visitor) => visitor.VisitGreaterThanOrEqPredicate(this);
    }
}
