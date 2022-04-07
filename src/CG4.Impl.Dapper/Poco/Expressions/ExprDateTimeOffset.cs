namespace CG4.Impl.Dapper.Poco.Expressions
{
    public class ExprDateTimeOffset : ExprConst
    {
        public DateTimeOffset Value { get; set; }

        public override void Accept(IExprVisitor visitor) => visitor.VisitDateTime(this);
    }
}
