namespace CG4.Impl.Dapper.Poco.Expressions
{
    public class ExprLong : ExprConst
    {
        public long Value { get; set; }

        public override void Accept(IExprVisitor visitor) => visitor.VisitLong(this);
    }
}
