using CG4.DataAccess.Poco.Visitors;

namespace CG4.DataAccess.Poco.Expressions
{
    public class ExprDateTimeOffset : ExprConst
    {
        public DateTimeOffset Value { get; set; }

        public override void Accept(IExprVisitor visitor) => visitor.VisitDateTimeOffset(this);
    }
}
