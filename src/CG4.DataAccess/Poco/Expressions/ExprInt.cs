using CG4.DataAccess.Poco.Visitors;

namespace CG4.DataAccess.Poco.Expressions
{
    public class ExprInt : ExprConst
    {
        public int Value { get; set; }

        public override void Accept(IExprVisitor visitor) => visitor.VisitInt(this);
    }
}
