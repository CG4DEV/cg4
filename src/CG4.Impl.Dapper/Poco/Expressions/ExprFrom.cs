namespace CG4.Impl.Dapper.Poco.Expressions
{
    public class ExprFrom : Expr
    {
        public ExprTableName TableName { get; set; }

        public ExprListJoins Joins { get; set; } = new ExprListJoins();

        public override void Accept(IExprVisitor visitor) => visitor.VisitFrom(this);
    }
}
