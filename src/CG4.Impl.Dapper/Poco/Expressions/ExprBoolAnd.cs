namespace CG4.Impl.Dapper.Poco.Expressions
{
    public class ExprBoolAnd : ExprBoolean
    {
        public ExprBoolean Left { get; set; }

        public ExprBoolean Right { get; set; }

        public override void Accept(IExprVisitor visitor) => visitor.VisitAnd(this);
    }
}
