using ITL.DataAccess.Poco.Visitors;

namespace ITL.DataAccess.Poco.Expressions
{
    public class ExprNull : ExprConst
    {
        public override void Accept(IExprVisitor visitor) => visitor.VisitNull(this);
    }
}
