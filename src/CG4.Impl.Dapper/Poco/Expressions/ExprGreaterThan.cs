namespace CG4.Impl.Dapper.Poco.Expressions
{
    public class ExprGreaterThan : ExprBinary
    {
        public override void Accept(IExprVisitor visitor) => visitor.VisitGreaterThanPredicate(this);
    }
}