namespace CG4.Impl.Dapper.Poco.Expressions
{
    public abstract class ExprBoolean : Expr
    {
        public static ExprBoolean operator &(ExprBoolean left, ExprBoolean right)
            => new ExprBoolAnd { Left = left, Right = right };

        public static ExprBoolean operator |(ExprBoolean left, ExprBoolean right)
            => new ExprBoolOr { Left = left, Right = right };
    }
}
