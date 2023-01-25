using CG4.DataAccess.Poco.Visitors;

namespace CG4.DataAccess.Poco.Expressions
{
    public class ExprStr : ExprConst
    {
        public string Value { get; set; }

        public override void Accept(IExprVisitor visitor) => visitor.VisitStr(this);
    }
}
