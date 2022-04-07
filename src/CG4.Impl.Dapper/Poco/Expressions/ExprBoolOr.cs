namespace CG4.Impl.Dapper.Poco.Expressions
{
    public class ExprBoolOr : ExprBoolean
    {
        public ExprBoolean Left { get; set; }

        public ExprBoolean Right { get; set; }

        public override void Accept(IExprVisitor visitor) => visitor.VisitOr(this);
    }
}
