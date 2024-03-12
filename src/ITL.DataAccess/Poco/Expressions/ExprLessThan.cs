using ITL.DataAccess.Poco.Visitors;

namespace ITL.DataAccess.Poco.Expressions
{
    public class ExprLessThan : ExprBinary
    {
        public override void Accept(IExprVisitor visitor) => visitor.VisitLessThanPredicate(this);
    }
}
