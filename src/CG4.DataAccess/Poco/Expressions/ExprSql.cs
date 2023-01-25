using CG4.DataAccess.Poco.Visitors;

namespace CG4.DataAccess.Poco.Expressions
{
    public class ExprSql : Expr
    {
        public ExprSelect Select { get; set; }

        public ExprFrom From { get; set; }

        public ExprWhere Where { get; set; }

        public ExprOrderBy OrderBy { get; set; }

        public int? Limit { get; set; }

        public int? Offset { get; set; }

        public override void Accept(IExprVisitor visitor) => visitor.VisitSql(this);
    }
}
