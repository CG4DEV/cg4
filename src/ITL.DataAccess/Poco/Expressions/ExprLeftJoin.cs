using ITL.DataAccess.Poco.Visitors;

namespace ITL.DataAccess.Poco.Expressions
{
    public class ExprLeftJoin : ExprJoin
    {
        public override void Accept(IExprVisitor visitor) => visitor.VisitLeftJoin(this);
    }
}
