namespace CG4.Impl.Dapper.Poco.Expressions
{
    public class ExprBoolNotEqPredicate : ExprBinary
    {
        public override void Accept(IExprVisitor visitor) => visitor.VisitNotEqPredicate(this);
    }
}
