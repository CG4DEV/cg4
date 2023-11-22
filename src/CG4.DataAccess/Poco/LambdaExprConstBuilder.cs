using CG4.DataAccess.Poco.Expressions;

namespace CG4.DataAccess.Poco
{
    internal class LambdaExprConstBuilder : IExprConstBuilder
    {
        private readonly Func<object, ExprConst> _build;

        public LambdaExprConstBuilder(Func<object, ExprConst> func)
        {
            ArgumentNullException.ThrowIfNull(func, nameof(func));

            _build = func;
        }

        public ExprConst Build(Type type, object value)
        {
            return _build.Invoke(value);
        }
    }
}
