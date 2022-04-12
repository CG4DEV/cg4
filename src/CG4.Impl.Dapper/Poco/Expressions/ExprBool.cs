namespace CG4.Impl.Dapper.Poco.Expressions
{
    public class ExprBool : ExprConst
    {
        public bool Value { get; set; }

        public override void Accept(IExprVisitor visitor) => visitor.VisitBool(this);
    }
}
