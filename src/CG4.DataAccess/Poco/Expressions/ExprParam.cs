using CG4.DataAccess.Poco.Visitors;

namespace CG4.DataAccess.Poco.Expressions
{
    public class ExprParam : ExprConst
    {
        public string Name { get; set; }

        public override void Accept(IExprVisitor visitor) => visitor.VisitParam(this);
    }
}
