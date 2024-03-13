using ITL.DataAccess.Poco.Visitors;

namespace ITL.DataAccess.Poco.Expressions
{
    public class ExprBoolEqPredicate : ExprBinary
    {
        public override void Accept(IExprVisitor visitor) => visitor.VisitEqPredicate(this);
    }
}
