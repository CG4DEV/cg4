using ITL.DataAccess.Poco.Visitors;

namespace ITL.DataAccess.Poco.Expressions
{
    public abstract class Expr
    {
        public abstract void Accept(IExprVisitor visitor);
    }
}
