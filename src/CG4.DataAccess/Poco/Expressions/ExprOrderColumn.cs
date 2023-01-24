using CG4.DataAccess.Poco.Visitors;

namespace CG4.DataAccess.Poco.Expressions
{
    public class ExprOrderColumn : ExprColumn
    {
        public ExprOrderColumn(bool ask)
        {
            Order = ask ? "ASC" : "DESC";
        }

        public ExprOrderColumn(ExprColumn column, bool ask)
        {
            Alias = column.Alias;
            Name = column.Name;
            Order = ask ? "ASC" : "DESC";
        }

        public string Order { get; set; }

        public override void Accept(IExprVisitor visitor) => visitor.VisitOrderColumn(this);
    }
}
