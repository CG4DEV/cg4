namespace CG4.Impl.Dapper.Poco.Expressions
{
    public class ExprSelectedColumn : ExprColumn
    {
        public string ResultName { get; set; }

        public override void Accept(IExprVisitor visitor) => visitor.VisitSelectedColumn(this);
    }
}
