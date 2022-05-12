using System.Linq.Expressions;
using CG4.Impl.Dapper.Poco.Expressions;

namespace CG4.Impl.Dapper.Poco
{
    public interface IClassSqlOptions<TEntity> : ISqlOption
        where TEntity : class
    {
        IClassSqlOptions<TEntity> Where(Expression<Func<TEntity, bool>> predicate);
        IClassSqlOptions<TEntity> Where(ExprBoolean predicate);

        IClassSqlOptions<TEntity> OrderBy<TKey>(Expression<Func<TEntity, TKey>> keySelector);

        IClassSqlOptions<TEntity> OrderByDesc<TKey>(Expression<Func<TEntity, TKey>> keySelector);

        IClassJoinSqlOptions<TEntity, TJoin> Join<TJoin, TKey>(Expression<Func<TEntity, TKey>> predicate, string alias)

            where TJoin : class;
        IClassJoinSqlOptions<TEntity, TJoin> JoinLeft<TJoin, TKey>(Expression<Func<TEntity, TKey>> predicate, string alias)
            where TJoin : class;

        IClassJoinSqlOptions<TEntity, TJoin> JoinRight<TJoin, TKey>(Expression<Func<TEntity, TKey>> predicate, string alias)
            where TJoin : class;
        IClassSqlOptions<TEntity> Offset(int offset);
        IClassSqlOptions<TEntity> Limit(int limit);
    }
}
