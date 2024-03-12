using ITL.DataAccess.Poco.Visitors;

namespace ITL.DataAccess.Poco.Expressions
{
    public class ExprDateTimeOffset : ExprConst
    {
        public DateTimeOffset Value { get; set; }

        public override void Accept(IExprVisitor visitor) => visitor.VisitDateTimeOffset(this);
    }
}
