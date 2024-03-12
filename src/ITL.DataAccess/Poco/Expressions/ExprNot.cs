using ITL.DataAccess.Poco.Visitors;

namespace ITL.DataAccess.Poco.Expressions
{
    public class ExprNot : ExprBoolFunc
    {
        public ExprBoolean Body { get; set; }

        public override void Accept(IExprVisitor visitor) => visitor.VisitNot(this);
    }
}
