namespace CG4.Impl.Dapper.Poco.Expressions
{
    public class ExprGreaterThanOrEq : ExprBinary
    {
        public override void Accept(IExprVisitor visitor) => visitor.VisitGreaterThanOrEqPredicate(this);
    }
}
