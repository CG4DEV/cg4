using CG4.DataAccess.Poco.Visitors;

namespace CG4.DataAccess.Poco.Expressions
{
    public class ExprLessThan : ExprBinary
    {
        public override void Accept(IExprVisitor visitor) => visitor.VisitLessThanPredicate(this);
    }
}
