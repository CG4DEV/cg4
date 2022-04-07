namespace CG4.Impl.Dapper.Poco.Expressions
{
    public abstract class Expr
    {
        public abstract void Accept(IExprVisitor visitor);
    }
}
