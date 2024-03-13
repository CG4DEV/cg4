using ITL.DataAccess.Poco.Visitors;

namespace ITL.DataAccess.Poco.Expressions
{
    public class ExprStr : ExprConst
    {
        public string Value { get; set; }

        public override void Accept(IExprVisitor visitor) => visitor.VisitStr(this);
    }
}
