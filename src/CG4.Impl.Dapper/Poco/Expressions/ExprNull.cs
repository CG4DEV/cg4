namespace CG4.Impl.Dapper.Poco.Expressions
{
    public class ExprNull : ExprConst
    {
        public override void Accept(IExprVisitor visitor) => visitor.VisitNull(this);
    }
}
