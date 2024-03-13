using ITL.DataAccess.Poco.Visitors;

namespace ITL.DataAccess.Poco.Expressions
{
    public class ExprInnerJoin : ExprJoin
    {
        public override void Accept(IExprVisitor visitor) => visitor.VisitInnerJoin(this);
    }
}
