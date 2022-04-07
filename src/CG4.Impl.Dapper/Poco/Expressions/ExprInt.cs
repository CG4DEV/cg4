namespace CG4.Impl.Dapper.Poco.Expressions
{
    public class ExprInt : ExprConst
    {
        public int Value { get; set; }

        public override void Accept(IExprVisitor visitor) => visitor.VisitInt(this);
    }
}
