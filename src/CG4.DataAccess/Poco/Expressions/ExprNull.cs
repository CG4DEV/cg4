using CG4.DataAccess.Poco.Visitors;

namespace CG4.DataAccess.Poco.Expressions
{
    public class ExprNull : ExprConst
    {
        public override void Accept(IExprVisitor visitor) => visitor.VisitNull(this);
    }
}
