using CG4.DataAccess.Poco.Expressions;

namespace CG4.DataAccess.Poco
{
    /// <summary>
    /// Registry for expression constant builders.
    /// </summary>
    public static class ExprConstBuildRegister
    {        private static readonly Dictionary<Type, IExprConstBuilder> _defaultBuilders = new()
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
            { typeof(decimal), new ExprConstLambdaBuilder(x => new ExprDecimal { Value = (decimal)x }) },

            { typeof(long?), new ExprConstLambdaBuilder(x => new ExprLong { Value = (long)x }) },
            { typeof(int?), new ExprConstLambdaBuilder(x => new ExprInt { Value = (int)x }) },
            { typeof(short?), new ExprConstLambdaBuilder(x => new ExprInt { Value = (short)x }) },
            { typeof(byte?), new ExprConstLambdaBuilder(x => new ExprInt { Value = (byte)x }) },
            { typeof(DateTime?), new ExprConstLambdaBuilder(x => new ExprDateTime { Value = (DateTime)x }) },
            { typeof(DateTimeOffset?), new ExprConstLambdaBuilder(x => new ExprDateTimeOffset { Value = (DateTimeOffset)x }) },
            { typeof(bool?), new ExprConstLambdaBuilder(x => new ExprBool { Value = (bool)x }) },
            { typeof(decimal?), new ExprConstLambdaBuilder(x => new ExprDecimal { Value = (decimal)x }) },
        };

        private static readonly Dictionary<Type, IExprConstBuilder> _builders = new();        internal static IExprConstBuilder? Get(Type type)
        {
            if (_builders.TryGetValue(type, out var customBuilder))
            {
                return customBuilder;
            }

            if (_defaultBuilders.TryGetValue(type, out var defaultBuilder))
            {
                return defaultBuilder;
            }

            return null;
        }

        /// <summary>
        /// Registers a builder for a specific type.
        /// </summary>
        /// <typeparam name="T">The type to register the builder for.</typeparam>
        /// <param name="builder">The expression constant builder.</param>
        public static void Register<T>(IExprConstBuilder builder)
        {
            Register(typeof(T), builder);
        }

        /// <summary>
        /// Registers a builder for a specific type.
        /// </summary>
        /// <param name="type">The type to register the builder for.</param>
        /// <param name="builder">The expression constant builder.</param>
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
