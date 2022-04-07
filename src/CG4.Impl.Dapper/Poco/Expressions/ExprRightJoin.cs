namespace CG4.Impl.Dapper.Poco.Expressions
{
    public class ExprRightJoin : ExprJoin
    {
        public override void Accept(IExprVisitor visitor) => visitor.VisitRightJoin(this);
    }
}
