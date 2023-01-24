using CG4.DataAccess.Poco.Visitors;

namespace CG4.DataAccess.Poco.Expressions
{
    public class ExprNot : ExprBoolFunc
    {
        public ExprBoolean Body { get; set; }

        public override void Accept(IExprVisitor visitor) => visitor.VisitNot(this);
    }   
}
