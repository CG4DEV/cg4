using CG4.DataAccess.Poco.Expressions;

namespace CG4.DataAccess.Poco
{
    /// <summary>
    /// A lambda-based implementation of IExprConstBuilder that uses a provided function to convert values to SQL expressions.
    /// Allows for flexible and concise definition of type conversions through lambda expressions.
    /// </summary>
    internal class ExprConstLambdaBuilder : IExprConstBuilder
    {
        private readonly Func<object, ExprConst> _build;

        /// <summary>
        /// Initializes a new instance of the ExprConstLambdaBuilder class.
        /// </summary>
        /// <param name="func">The function that performs the conversion from a value to a SQL expression constant.</param>
        /// <exception cref="ArgumentNullException">Thrown when the provided function is null.</exception>
        public ExprConstLambdaBuilder(Func<object, ExprConst> func)
        {
            ArgumentNullException.ThrowIfNull(func, nameof(func));

            _build = func;
        }

        /// <summary>
        /// Builds a SQL expression constant by invoking the provided conversion function.
        /// </summary>
        /// <param name="type">The .NET type of the value being converted.</param>
        /// <param name="value">The value to convert to a SQL expression constant.</param>
        /// <returns>A SQL expression constant representing the value.</returns>
        public ExprConst Build(Type type, object value)
        {
            return _build.Invoke(value);
        }
    }
}
