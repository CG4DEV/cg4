using CG4.DataAccess.Poco.Expressions;

namespace CG4.DataAccess.Poco
{
    /// <summary>
    /// Registry for expression constant builders that handle conversions of .NET types to SQL expression types.
    /// Provides a centralized mechanism for registering and retrieving type-specific expression builders.
    /// </summary>
    /// <remarks>
    /// This class maintains two sets of builders:
    /// 1. Default builders for common .NET types (string, numeric types, DateTime, etc.)
    /// 2. Custom builders that can be registered for specific types
    /// </remarks>
    public static class ExprConstBuildRegister
    {
        private static readonly Dictionary<Type, IExprConstBuilder> _defaultBuilders = new()
        {
            { typeof(string), new ExprConstLambdaBuilder(x => new ExprStr { Value = (string)x }) },
            { typeof(long), new ExprConstLambdaBuilder(x => new ExprLong { Value = (long)x }) },
            { typeof(int), new ExprConstLambdaBuilder(x => new ExprInt { Value = (int)x }) },
            { typeof(short), new ExprConstLambdaBuilder(x => new ExprInt { Value = (short)x }) },
            { typeof(byte), new ExprConstLambdaBuilder(x => new ExprInt { Value = (byte)x }) },
            { typeof(bool), new ExprConstLambdaBuilder(x => new ExprBool { Value = (bool)x }) },
            { typeof(decimal), new ExprConstLambdaBuilder(x => new ExprDecimal { Value = (decimal)x }) },
            { typeof(DateTime), new ExprConstLambdaBuilder(x => new ExprDateTime { Value = (DateTime)x }) },
            { typeof(DateTimeOffset), new ExprConstLambdaBuilder(x => new ExprDateTimeOffset { Value = (DateTimeOffset)x }) },
            { typeof(Guid), new ExprConstLambdaBuilder(x => new ExprGuid { Value = (Guid)x }) },

            { typeof(long?), new ExprConstLambdaBuilder(x => new ExprLong { Value = (long)x }) },
            { typeof(int?), new ExprConstLambdaBuilder(x => new ExprInt { Value = (int)x }) },
            { typeof(short?), new ExprConstLambdaBuilder(x => new ExprInt { Value = (short)x }) },
            { typeof(byte?), new ExprConstLambdaBuilder(x => new ExprInt { Value = (byte)x }) },
            { typeof(bool?), new ExprConstLambdaBuilder(x => new ExprBool { Value = (bool)x }) },
            { typeof(decimal?), new ExprConstLambdaBuilder(x => new ExprDecimal { Value = (decimal)x }) },
            { typeof(DateTime?), new ExprConstLambdaBuilder(x => new ExprDateTime { Value = (DateTime)x }) },
            { typeof(DateTimeOffset?), new ExprConstLambdaBuilder(x => new ExprDateTimeOffset { Value = (DateTimeOffset)x }) },
        };

        private static readonly Dictionary<Type, IExprConstBuilder> _builders = new();

        /// <summary>
        /// Retrieves the expression constant builder for a specific type.
        /// First checks for custom builders, then falls back to default builders.
        /// </summary>
        /// <param name="type">The type to get the builder for.</param>
        /// <returns>
        /// The registered builder for the type, or null if no builder is found.
        /// Custom builders take precedence over default builders.
        /// </returns>
        internal static IExprConstBuilder? Get(Type type)
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
        /// Registers a custom expression constant builder for a specific type.
        /// </summary>
        /// <typeparam name="T">The type to register the builder for.</typeparam>
        /// <param name="builder">The expression constant builder that handles conversion of type T to SQL expressions.</param>
        /// <exception cref="InvalidOperationException">Thrown when a builder is already registered for the specified type.</exception>
        public static void Register<T>(IExprConstBuilder builder)
        {
            Register(typeof(T), builder);
        }

        /// <summary>
        /// Registers a custom expression constant builder for a specific type.
        /// </summary>
        /// <param name="type">The type to register the builder for.</param>
        /// <param name="builder">The expression constant builder that handles conversion to SQL expressions.</param>
        /// <exception cref="InvalidOperationException">Thrown when a builder is already registered for the specified type.</exception>
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
