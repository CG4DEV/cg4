namespace CG4.Impl.Dapper.Poco.Expressions
{
    public abstract class ExprConst : Expr
    {
        private static readonly Dictionary<Type, Func<object, ExprConst>> _builders = new()
        {
            { typeof(string), x => new ExprStr { Value = (string)x } },
            { typeof(long), x => new ExprLong { Value = (long)x } },
            { typeof(int), x => new ExprInt { Value = (int)x } },
            { typeof(DateTimeOffset), x => new ExprDateTimeOffset { Value = (DateTimeOffset)x } },
        };

        public static ExprConst Create(Type valueType, object value)
        {
            return _builders[valueType].Invoke(value);
        }
    }
}
