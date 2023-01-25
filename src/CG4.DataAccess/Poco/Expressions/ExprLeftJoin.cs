using CG4.DataAccess.Poco.Visitors;

namespace CG4.DataAccess.Poco.Expressions
{
    public class ExprLeftJoin : ExprJoin
    {
        public override void Accept(IExprVisitor visitor) => visitor.VisitLeftJoin(this);
    }
}
