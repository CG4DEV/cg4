using CG4.DataAccess.Poco.Visitors;

namespace CG4.DataAccess.Poco.Expressions
{
    public abstract class Expr
    {
        public abstract void Accept(IExprVisitor visitor);
    }
}
