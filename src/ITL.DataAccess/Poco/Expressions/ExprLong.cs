using ITL.DataAccess.Poco.Visitors;

namespace ITL.DataAccess.Poco.Expressions
{
    public class ExprLong : ExprConst
    {
        public long Value { get; set; }

        public override void Accept(IExprVisitor visitor) => visitor.VisitLong(this);
    }
}
