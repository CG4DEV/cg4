using ITL.DataAccess.Poco.Visitors;

namespace ITL.DataAccess.Poco.Expressions
{
    public class ExprInt : ExprConst
    {
        public int Value { get; set; }

        public override void Accept(IExprVisitor visitor) => visitor.VisitInt(this);
    }
}
