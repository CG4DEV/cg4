using System.Linq.Expressions;

namespace CG4.Impl.Dapper.Poco
{
    public interface ISqlBuilder
    {
        string GetById<T>(Expression<Action<IClassSqlOptions<T>>> predicate = null)
            where T : class;

        string GetAll<T>(Expression<Action<IClassSqlOptions<T>>> predicate = null)
            where T : class;

        string Insert<T>()
            where T : class;

        string UpdateById<T>()
            where T : class;

        string DeleteById<T>()
            where T : class;
    }
}