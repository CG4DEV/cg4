using ITL.DataAccess.Poco.Visitors;

namespace ITL.DataAccess.Poco.Expressions
{
    public class ExprGreaterThan : ExprBinary
    {
        public override void Accept(IExprVisitor visitor) => visitor.VisitGreaterThanPredicate(this);
    }
}
