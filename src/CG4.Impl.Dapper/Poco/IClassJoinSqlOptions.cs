using System.Linq.Expressions;

namespace CG4.Impl.Dapper.Poco
{
    public interface IClassJoinSqlOptions<TEntity, TJoin> : ISqlOption
        where TEntity : class
        where TJoin : class
    {
        IClassJoinSqlOptions<TEntity, TJoin> OrderBy<TKey>(Expression<Func<TJoin, TKey>> keySelector);

        IClassJoinSqlOptions<TEntity, TJoin> OrderByDesc<TKey>(Expression<Func<TJoin, TKey>> keySelector);

        IClassJoinSqlOptions<TEntity, TNewJoin> Join<TNewJoin, TKey>(Expression<Func<TEntity, TKey>> predicate, string alias)
            where TNewJoin : class;

        IClassJoinSqlOptions<TEntity, TNewJoin> JoinLeft<TNewJoin, TKey>(Expression<Func<TEntity, TKey>> predicate, string alias)
            where TNewJoin : class;

        IClassJoinSqlOptions<TEntity, TNewJoin> JoinRight<TNewJoin, TKey>(Expression<Func<TEntity, TKey>> predicate, string alias)
            where TNewJoin : class;

        IClassJoinSqlOptions<TEntity, TJoin> Where(Expression<Func<TJoin, bool>> predicate);

        IClassSqlOptions<TEntity> ToBackMain();
    }
}
