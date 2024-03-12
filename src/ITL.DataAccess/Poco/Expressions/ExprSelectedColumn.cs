using ITL.DataAccess.Poco.Visitors;

namespace ITL.DataAccess.Poco.Expressions
{
    public class ExprSelectedColumn : ExprColumn
    {
        public string ResultName { get; set; }

        public override void Accept(IExprVisitor visitor) => visitor.VisitSelectedColumn(this);
    }
}
