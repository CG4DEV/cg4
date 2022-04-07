namespace CG4.Impl.Dapper.Poco.Expressions
{
    public class ExprParam : ExprConst
    {
        public string Name { get; set; }

        public override void Accept(IExprVisitor visitor) => visitor.VisitParam(this);
    }
}
