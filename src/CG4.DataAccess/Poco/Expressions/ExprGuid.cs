using CG4.DataAccess.Poco.Visitors;

namespace CG4.DataAccess.Poco.Expressions
{
    public class ExprGuid : ExprConst
    {
        public Guid Value { get; set; }

        public override void Accept(IExprVisitor visitor) => visitor.VisitGuid(this);
    }
}
