using ITL.DataAccess.Poco.Visitors;

namespace ITL.DataAccess.Poco.Expressions
{
    public class ExprBoolNotEqPredicate : ExprBinary
    {
        public override void Accept(IExprVisitor visitor) => visitor.VisitNotEqPredicate(this);
    }
}
