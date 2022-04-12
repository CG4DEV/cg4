namespace CG4.Impl.Dapper.Poco.Expressions
{
    public class ExprStar : Expr
    {
        public override void Accept(IExprVisitor visitor) => visitor.VisitStar(this);
    }
}
