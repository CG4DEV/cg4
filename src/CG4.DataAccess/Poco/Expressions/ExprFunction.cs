using CG4.DataAccess.Poco.Visitors;

namespace CG4.DataAccess.Poco.Expressions
{
    public abstract class ExprFunction : Expr
    {
        public abstract string Name { get; }

        public string ResultName { get; set; }

        public List<Expr> Parametrs { get; } = new();

        public override void Accept(IExprVisitor visitor) => visitor.VisitFunction(this);
    }
}
