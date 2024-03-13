using ITL.DataAccess.Poco.Expressions;

namespace ITL.DataAccess.Poco
{
    public static class ExprConstBuildRegister
    {
        private static readonly Dictionary<Type, IExprConstBuilder> _defaultBuilders = new()
        {
            { typeof(string), new LambdaExprConstBuilder(x => new ExprStr { Value = (string)x }) },
            { typeof(long), new LambdaExprConstBuilder(x => new ExprLong { Value = (long)x }) },
            { typeof(int), new LambdaExprConstBuilder(x => new ExprInt { Value = (int)x }) },
            { typeof(short), new LambdaExprConstBuilder(x => new ExprInt { Value = (short)x }) },
            { typeof(byte), new LambdaExprConstBuilder(x => new ExprInt { Value = (byte)x }) },
            { typeof(DateTime), new LambdaExprConstBuilder(x => new ExprDateTime { Value = (DateTime)x }) },
            { typeof(DateTimeOffset), new LambdaExprConstBuilder(x => new ExprDateTimeOffset { Value = (DateTimeOffset)x }) },
            { typeof(bool), new LambdaExprConstBuilder(x => new ExprBool { Value = (bool)x }) },

            { typeof(long?), new LambdaExprConstBuilder(x => new ExprLong { Value = (long)x }) },
            { typeof(int?), new LambdaExprConstBuilder(x => new ExprInt { Value = (int)x }) },
            { typeof(short?), new LambdaExprConstBuilder(x => new ExprInt { Value = (short)x }) },
            { typeof(byte?), new LambdaExprConstBuilder(x => new ExprInt { Value = (byte)x }) },
            { typeof(DateTime?), new LambdaExprConstBuilder(x => new ExprDateTime { Value = (DateTime)x }) },
            { typeof(DateTimeOffset?), new LambdaExprConstBuilder(x => new ExprDateTimeOffset { Value = (DateTimeOffset)x }) },
            { typeof(bool?), new LambdaExprConstBuilder(x => new ExprBool { Value = (bool)x }) },
        };

        private static readonly Dictionary<Type, IExprConstBuilder> _builders = new();

        internal static IExprConstBuilder? Get(Type type)
        {
            IExprConstBuilder builder;

            if (_builders.TryGetValue(type, out builder))
            {
                return builder;
            }

            if (_defaultBuilders.TryGetValue(type, out builder))
            {
                return builder;
            }

            return null;
        }

        public static void Register<T>(IExprConstBuilder builder)
        {
            Register(typeof(T), builder);
        }

        public static void Register(Type type, IExprConstBuilder builder)
        {
            if (_builders.ContainsKey(type))
            {
                throw new InvalidOperationException("Builder alredy registred");
            }

            _builders.Add(type, builder);
        }
    }
}
