using ITL.DataAccess.Poco.Visitors;

namespace ITL.DataAccess.Poco.Expressions
{
    public class ExprLessThanOrEq : ExprBinary
    {
        public override void Accept(IExprVisitor visitor) => visitor.VisitLessThanOrEqPredicate(this);
    }
}
