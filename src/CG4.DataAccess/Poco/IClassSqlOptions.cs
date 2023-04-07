using System.Linq.Expressions;
using CG4.DataAccess.Poco.Expressions;

namespace CG4.DataAccess.Poco
{
    public interface IClassSqlOptions<TEntity> : ISqlOption
        where TEntity : class
    {
        IClassSqlOptions<TEntity> As(string tableAlias);

        IClassSqlOptions<TEntity> Where(Expression<Func<TEntity, bool>> predicate);

        IClassSqlOptions<TEntity> Where(ExprBoolean predicate);

        /// <summary>
        /// Append where conditions without alias updates
        /// </summary>
        /// <param name="predicate">Additional conditions</param>
        IClassSqlOptions<TEntity> AppendWhere(ExprBoolean predicate);

        IClassSqlOptions<TEntity> OrderBy<TKey>(Expression<Func<TEntity, TKey>> keySelector);

        IClassSqlOptions<TEntity> OrderBy<TKey>(Expression<Func<TEntity, TKey>> keySelector, bool ask);

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
