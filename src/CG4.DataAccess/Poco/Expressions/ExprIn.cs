using CG4.DataAccess.Poco.Visitors;

namespace CG4.DataAccess.Poco.Expressions
{
    public class ExprIn : ExprBoolFunc
    {
        public ExprColumn Column { get; set; }

        public ExprArray Values { get; set; }

        public override void Accept(IExprVisitor visitor) => visitor.VisitIn(this);
    }
}
