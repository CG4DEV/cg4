using CG4.DataAccess.Poco.Expressions;

namespace CG4.DataAccess.Poco
{
    public interface IExprConstBuilder
    {
        public abstract ExprConst Build(Type type, object value);
    }
}
