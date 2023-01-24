using CG4.DataAccess.Poco.Visitors;

namespace CG4.DataAccess.Poco.Expressions
{
    public class ExprBool : ExprConst
    {
        public bool Value { get; set; }

        public override void Accept(IExprVisitor visitor) => visitor.VisitBool(this);
    }
}
