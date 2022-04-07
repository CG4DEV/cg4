namespace CG4.Impl.Dapper.Poco.Expressions
{
    public class ExprStr : ExprConst
    {
        public string Value { get; set; }

        public override void Accept(IExprVisitor visitor) => visitor.VisitStr(this);
    }
}
