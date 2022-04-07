namespace CG4.Impl.Dapper.Poco.Expressions
{
    public class ExprLeftJoin : ExprJoin
    {
        public override void Accept(IExprVisitor visitor) => visitor.VisitLeftJoin(this);
    }
}
