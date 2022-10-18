namespace CG4.Impl.Dapper.Poco.Expressions
{
    public class ExprLessThanOrEq : ExprBinary
    {
        public override void Accept(IExprVisitor visitor) => visitor.VisitLessThanOrEqPredicate(this);
    }
}
