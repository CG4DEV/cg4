using CG4.DataAccess.Poco.Visitors;

namespace CG4.DataAccess.Poco.Expressions
{
    public class ExprGreaterThan : ExprBinary
    {
        public override void Accept(IExprVisitor visitor) => visitor.VisitGreaterThanPredicate(this);
    }
}
