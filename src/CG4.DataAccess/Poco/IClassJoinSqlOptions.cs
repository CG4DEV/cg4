using System.Linq.Expressions;
using CG4.DataAccess.Poco.Expressions;

namespace CG4.DataAccess.Poco
{
    /// <summary>
    /// Provides a fluent interface for configuring SQL JOIN operations and related query options.
    /// Extends the base query building capabilities with join-specific functionality.
    /// </summary>
    /// <typeparam name="TEntity">The primary entity type in the query.</typeparam>
    /// <typeparam name="TJoin">The type of entity being joined.</typeparam>
    public interface IClassJoinSqlOptions<TEntity, TJoin> : ISqlOption
        where TEntity : class
        where TJoin : class
    {
        /// <summary>
        /// Sets an alias for the joined table in the SQL query.
        /// </summary>
        /// <param name="tableAlias">The alias name to use for the joined table.</param>
        /// <returns>The join query builder instance for method chaining.</returns>
        IClassJoinSqlOptions<TEntity, TJoin> As(string tableAlias);

        /// <summary>
        /// Adds an ORDER BY clause to the query based on a property of the joined entity.
        /// </summary>
        /// <typeparam name="TKey">The type of the property to sort by.</typeparam>
        /// <param name="keySelector">Expression selecting the property to sort by.</param>
        /// <returns>The join query builder instance for method chaining.</returns>
        IClassJoinSqlOptions<TEntity, TJoin> OrderBy<TKey>(Expression<Func<TJoin, TKey>> keySelector);

        /// <summary>
        /// Adds an ORDER BY clause with specified sort direction based on a property of the joined entity.
        /// </summary>
        /// <typeparam name="TKey">The type of the property to sort by.</typeparam>
        /// <param name="keySelector">Expression selecting the property to sort by.</param>
        /// <param name="asc">True for ascending order, false for descending.</param>
        /// <returns>The join query builder instance for method chaining.</returns>
        IClassJoinSqlOptions<TEntity, TJoin> OrderBy<TKey>(Expression<Func<TJoin, TKey>> keySelector, bool asc);

        /// <summary>
        /// Adds a descending ORDER BY clause based on a property of the joined entity.
        /// </summary>
        /// <typeparam name="TKey">The type of the property to sort by.</typeparam>
        /// <param name="keySelector">Expression selecting the property to sort by.</param>
        /// <returns>The join query builder instance for method chaining.</returns>
        IClassJoinSqlOptions<TEntity, TJoin> OrderByDesc<TKey>(Expression<Func<TJoin, TKey>> keySelector);

        /// <summary>
        /// Adds an additional INNER JOIN to the query.
        /// </summary>
        /// <typeparam name="TNewJoin">The type of entity to join with.</typeparam>
        /// <typeparam name="TKey">The type of the join key.</typeparam>
        /// <param name="predicate">Expression defining the join condition.</param>
        /// <param name="alias">Alias for the newly joined table.</param>
        /// <returns>A new join query builder for the additional join.</returns>
        IClassJoinSqlOptions<TEntity, TNewJoin> Join<TNewJoin, TKey>(Expression<Func<TEntity, TKey>> predicate, string alias)
            where TNewJoin : class;

        /// <summary>
        /// Adds an additional LEFT JOIN to the query.
        /// </summary>
        /// <typeparam name="TNewJoin">The type of entity to join with.</typeparam>
        /// <typeparam name="TKey">The type of the join key.</typeparam>
        /// <param name="predicate">Expression defining the join condition.</param>
        /// <param name="alias">Alias for the newly joined table.</param>
        /// <returns>A new join query builder for the additional join.</returns>
        IClassJoinSqlOptions<TEntity, TNewJoin> JoinLeft<TNewJoin, TKey>(Expression<Func<TEntity, TKey>> predicate, string alias)
            where TNewJoin : class;

        /// <summary>
        /// Adds an additional RIGHT JOIN to the query.
        /// </summary>
        /// <typeparam name="TNewJoin">The type of entity to join with.</typeparam>
        /// <typeparam name="TKey">The type of the join key.</typeparam>
        /// <param name="predicate">Expression defining the join condition.</param>
        /// <param name="alias">Alias for the newly joined table.</param>
        /// <returns>A new join query builder for the additional join.</returns>
        IClassJoinSqlOptions<TEntity, TNewJoin> JoinRight<TNewJoin, TKey>(Expression<Func<TEntity, TKey>> predicate, string alias)
            where TNewJoin : class;

        /// <summary>
        /// Adds a WHERE clause to filter the results of the query based on a condition applied to the joined entity.
        /// </summary>
        /// <param name="predicate">Expression defining the filter condition.</param>
        /// <returns>The join query builder instance for method chaining.</returns>
        IClassJoinSqlOptions<TEntity, TJoin> Where(Expression<Func<TJoin, bool>> predicate);

        /// <summary>
        /// Adds a WHERE clause to filter the results of the query based on a boolean expression.
        /// </summary>
        /// <param name="predicate">The boolean expression representing the filter condition.</param>
        /// <returns>The join query builder instance for method chaining.</returns>
        IClassJoinSqlOptions<TEntity, TJoin> Where(ExprBoolean predicate);

        /// <summary>
        /// Append where conditions without alias updates
        /// </summary>
        /// <param name="predicate">Additional conditions</param>
        /// <returns>The join query builder instance for method chaining.</returns>
        IClassJoinSqlOptions<TEntity, TJoin> AppendWhere(ExprBoolean predicate);

        /// <summary>
        /// Transitions the query context back to the main entity, ending the join configuration.
        /// </summary>
        /// <returns>An options interface for configuring the main entity query.</returns>
        IClassSqlOptions<TEntity> ToBackMain();
    }
}
