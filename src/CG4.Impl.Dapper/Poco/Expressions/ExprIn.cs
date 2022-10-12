namespace CG4.Impl.Dapper.Poco.Expressions
{
    public class ExprIn : ExprBoolFunc
    {
        public ExprColumn Column { get; set; }

        public ExprArray Values { get; set; }

        public override void Accept(IExprVisitor visitor) => visitor.VisitIn(this);
    }
}
