using CG4.DataAccess.Poco.Visitors;

namespace CG4.DataAccess.Poco.Expressions
{
    public class ExprDateTime : ExprConst
    {
        public DateTime Value { get; set; }

        public override void Accept(IExprVisitor visitor) => visitor.VisitDateTime(this);
    }
}
