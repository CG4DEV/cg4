using ITL.DataAccess.Poco.Expressions;

namespace ITL.DataAccess.Poco
{
    public interface IExprConstBuilder
    {
        public abstract ExprConst Build(Type type, object value);
    }
}
