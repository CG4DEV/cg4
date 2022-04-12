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
            { typeof(bool), x => new ExprBool { Value = (bool)x } },

            { typeof(long?), x => new ExprLong { Value = (long)x } },
            { typeof(int?), x => new ExprInt { Value = (int)x } },
            { typeof(DateTimeOffset?), x => new ExprDateTimeOffset { Value = (DateTimeOffset)x } },
            { typeof(bool?), x => new ExprBool { Value = (bool)x } },
        };

        public static ExprConst Create(Type valueType, object value)
        {
            if (value == null)
            {
                return new ExprNull();
            }

            if (valueType.IsEnum)
            {
                return _builders[typeof(int)].Invoke(value);
            }

            return _builders[valueType].Invoke(value);
        }
    }
}
