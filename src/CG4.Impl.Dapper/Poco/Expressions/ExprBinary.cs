namespace CG4.Impl.Dapper.Poco.Expressions
{
    public abstract class ExprBinary : ExprBoolean
    {
        public ExprColumn Column { get; set; }

        public ExprConst Value { get; set; }
    }
}
