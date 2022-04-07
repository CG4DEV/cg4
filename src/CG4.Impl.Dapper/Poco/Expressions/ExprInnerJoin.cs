namespace CG4.Impl.Dapper.Poco.Expressions
{
    public class ExprInnerJoin : ExprJoin
    {
        public override void Accept(IExprVisitor visitor) => visitor.VisitInnerJoin(this);
    }
}
