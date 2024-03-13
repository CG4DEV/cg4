using ITL.DataAccess.Poco.Visitors;

namespace ITL.DataAccess.Poco.Expressions
{
    public class ExprRightJoin : ExprJoin
    {
        public override void Accept(IExprVisitor visitor) => visitor.VisitRightJoin(this);
    }
}
