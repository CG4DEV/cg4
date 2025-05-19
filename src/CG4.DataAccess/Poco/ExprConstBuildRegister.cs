using CG4.DataAccess.Poco.Expressions;

namespace CG4.DataAccess.Poco
{
    public static class ExprConstBuildRegister
    {
        private static readonly Dictionary<Type, IExprConstBuilder> _defaultBuilders = new()
        {
            { typeof(string), new ExprConstLambdaBuilder(x => new ExprStr { Value = (string)x }) },
            { typeof(long), new ExprConstLambdaBuilder(x => new ExprLong { Value = (long)x }) },
            { typeof(int), new ExprConstLambdaBuilder(x => new ExprInt { Value = (int)x }) },
            { typeof(short), new ExprConstLambdaBuilder(x => new ExprInt { Value = (short)x }) },
            { typeof(byte), new ExprConstLambdaBuilder(x => new ExprInt { Value = (byte)x }) },
            { typeof(DateTime), new ExprConstLambdaBuilder(x => new ExprDateTime { Value = (DateTime)x }) },
            { typeof(DateTimeOffset), new ExprConstLambdaBuilder(x => new ExprDateTimeOffset { Value = (DateTimeOffset)x }) },
            { typeof(bool), new ExprConstLambdaBuilder(x => new ExprBool { Value = (bool)x }) },
            { typeof(Guid), new ExprConstLambdaBuilder(x => new ExprGuid { Value = (Guid)x }) },

            { typeof(long?), new ExprConstLambdaBuilder(x => new ExprLong { Value = (long)x }) },
            { typeof(int?), new ExprConstLambdaBuilder(x => new ExprInt { Value = (int)x }) },
            { typeof(short?), new ExprConstLambdaBuilder(x => new ExprInt { Value = (short)x }) },
            { typeof(byte?), new ExprConstLambdaBuilder(x => new ExprInt { Value = (byte)x }) },
            { typeof(DateTime?), new ExprConstLambdaBuilder(x => new ExprDateTime { Value = (DateTime)x }) },
            { typeof(DateTimeOffset?), new ExprConstLambdaBuilder(x => new ExprDateTimeOffset { Value = (DateTimeOffset)x }) },
            { typeof(bool?), new ExprConstLambdaBuilder(x => new ExprBool { Value = (bool)x }) },
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
