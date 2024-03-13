using ITL.DataAccess.Poco.Visitors;

namespace ITL.DataAccess.Poco.Expressions
{
    public class ExprLike : ExprBoolFunc
    {
        public ExprColumn Column { get; set; }

        public string StartsPattern { get; set; } = string.Empty;

        public string EndsPattern { get; set; } = string.Empty;

        public string Value { get; set; }

        public override void Accept(IExprVisitor visitor) => visitor.VisitLike(this);
    }
}
