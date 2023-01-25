using CG4.DataAccess.Poco.Visitors;

namespace CG4.DataAccess.Poco.Expressions
{
    public class ExprLong : ExprConst
    {
        public long Value { get; set; }

        public override void Accept(IExprVisitor visitor) => visitor.VisitLong(this);
    }
}
