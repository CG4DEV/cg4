using CG4.DataAccess.Poco.Visitors;

namespace CG4.DataAccess.Poco.Expressions
{
    public class ExprSelectedColumn : ExprColumn
    {
        public string ResultName { get; set; }

        public override void Accept(IExprVisitor visitor) => visitor.VisitSelectedColumn(this);
    }
}
