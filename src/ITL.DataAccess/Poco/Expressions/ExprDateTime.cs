using ITL.DataAccess.Poco.Visitors;

namespace ITL.DataAccess.Poco.Expressions
{
    public class ExprDateTime : ExprConst
    {
        public DateTime Value { get; set; }

        public override void Accept(IExprVisitor visitor) => visitor.VisitDateTime(this);
    }
}
