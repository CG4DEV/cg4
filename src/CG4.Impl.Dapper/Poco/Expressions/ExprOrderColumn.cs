namespace CG4.Impl.Dapper.Poco.Expressions
{
    public class ExprOrderColumn : ExprColumn
    {
        public ExprOrderColumn(bool ask)
        {
            Order = ask ? "ASK" : "DESC";
        }

        public ExprOrderColumn(ExprColumn column, bool ask)
        {
            Alias = column.Alias;
            Name = column.Name;
            Order = ask ? "ASK" : "DESC";
        }

        public string Order { get; set; }

        public override void Accept(IExprVisitor visitor) => visitor.VisitOrderColumn(this);
    }
}
