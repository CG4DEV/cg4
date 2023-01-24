using CG4.DataAccess.Poco.Visitors;

namespace CG4.DataAccess.Poco.Expressions
{
    public class ExprInnerJoin : ExprJoin
    {
        public override void Accept(IExprVisitor visitor) => visitor.VisitInnerJoin(this);
    }
}
