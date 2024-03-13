using ITL.DataAccess.Poco.Visitors;

namespace ITL.DataAccess.Poco.Expressions
{
    public class ExprBool : ExprConst
    {
        public bool Value { get; set; }

        public override void Accept(IExprVisitor visitor) => visitor.VisitBool(this);
    }
}
