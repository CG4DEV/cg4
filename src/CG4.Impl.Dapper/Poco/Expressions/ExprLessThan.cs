namespace CG4.Impl.Dapper.Poco.Expressions
{
    public class ExprLessThan : ExprBinary
    {
        public override void Accept(IExprVisitor visitor) => visitor.VisitLessThanPredicate(this);
    }
}