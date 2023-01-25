using System.Linq.Expressions;
using CG4.DataAccess.Poco.Expressions;

namespace CG4.DataAccess.Poco
{
    public interface IClassJoinSqlOptions<TEntity, TJoin> : ISqlOption
        where TEntity : class
        where TJoin : class
    {
        IClassJoinSqlOptions<TEntity, TJoin> As(string tableAlias);

        IClassJoinSqlOptions<TEntity, TJoin> OrderBy<TKey>(Expression<Func<TJoin, TKey>> keySelector);

        IClassJoinSqlOptions<TEntity, TJoin> OrderByDesc<TKey>(Expression<Func<TJoin, TKey>> keySelector);

        IClassJoinSqlOptions<TEntity, TNewJoin> Join<TNewJoin, TKey>(Expression<Func<TEntity, TKey>> predicate, string alias)
            where TNewJoin : class;

        IClassJoinSqlOptions<TEntity, TNewJoin> JoinLeft<TNewJoin, TKey>(Expression<Func<TEntity, TKey>> predicate, string alias)
            where TNewJoin : class;

        IClassJoinSqlOptions<TEntity, TNewJoin> JoinRight<TNewJoin, TKey>(Expression<Func<TEntity, TKey>> predicate, string alias)
            where TNewJoin : class;

        IClassJoinSqlOptions<TEntity, TJoin> Where(Expression<Func<TJoin, bool>> predicate);

        IClassJoinSqlOptions<TEntity, TJoin> Where(ExprBoolean predicate);

        /// <summary>
        /// Append where conditions without alias updates
        /// </summary>
        /// <param name="predicate">Additional conditions</param>
        IClassJoinSqlOptions<TEntity, TJoin> AppendWhere(ExprBoolean predicate);

        IClassSqlOptions<TEntity> ToBackMain();
    }
}
