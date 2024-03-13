using ITL.DataAccess.Poco.Visitors;

namespace ITL.DataAccess.Poco.Expressions
{
    public class ExprWhere : Expr
    {
        public ExprBoolean Expression { get; set; }

        public void And(ExprBoolean expr)
        {
            if (Expression == null)
            {
                Expression = expr;
            }
            else
            {
                Expression = Expression & expr;
            }
        }

        public void Or(ExprBoolean expr)
        {
            if (Expression == null)
            {
                Expression = expr;
            }
            else
            {
                Expression = Expression | expr;
            }
        }

        public override void Accept(IExprVisitor visitor) => visitor.VisitWhere(this);
    }
}
