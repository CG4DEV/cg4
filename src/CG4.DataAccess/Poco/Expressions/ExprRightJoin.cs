using CG4.DataAccess.Poco.Visitors;

namespace CG4.DataAccess.Poco.Expressions
{
    public class ExprRightJoin : ExprJoin
    {
        public override void Accept(IExprVisitor visitor) => visitor.VisitRightJoin(this);
    }
}
