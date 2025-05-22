using System.Linq.Expressions;
using CG4.DataAccess.Poco.Expressions;

namespace CG4.DataAccess.Poco
{
    /// <summary>
    /// Provides a fluent interface for building SQL queries against a specific entity type.
    /// Supports common SQL operations like WHERE, ORDER BY, and table aliasing.
    /// </summary>
    /// <typeparam name="TEntity">The entity type to build queries for.</typeparam>
    public interface IClassSqlOptions<TEntity> : ISqlOption
        where TEntity : class
    {
        /// <summary>
        /// Sets an alias for the entity's table in the SQL query.
        /// </summary>
        /// <param name="tableAlias">The alias name to use for the table.</param>
        /// <returns>The query builder instance for method chaining.</returns>
        IClassSqlOptions<TEntity> As(string tableAlias);

        /// <summary>
        /// Adds a WHERE clause to the query using a LINQ expression.
        /// </summary>
        /// <param name="predicate">A LINQ expression representing the WHERE condition.</param>
        /// <returns>The query builder instance for method chaining.</returns>
        IClassSqlOptions<TEntity> Where(Expression<Func<TEntity, bool>> predicate);

        /// <summary>
        /// Adds a WHERE clause to the query using a SQL expression.
        /// </summary>
        /// <param name="predicate">A SQL boolean expression representing the WHERE condition.</param>
        /// <returns>The query builder instance for method chaining.</returns>
        IClassSqlOptions<TEntity> Where(ExprBoolean predicate);

        /// <summary>
        /// Appends additional WHERE conditions to the existing conditions without updating aliases.
        /// Useful for adding complex conditions that have already been properly aliased.
        /// </summary>
        /// <param name="predicate">Additional WHERE conditions to append.</param>
        /// <returns>The query builder instance for method chaining.</returns>
        IClassSqlOptions<TEntity> AppendWhere(ExprBoolean predicate);

        /// <summary>
        /// Adds an ORDER BY clause to the query.
        /// </summary>
        /// <typeparam name="TKey">The type of the property to sort by.</typeparam>
        /// <param name="keySelector">Expression selecting the property to sort by.</param>
        /// <returns>The query builder instance for method chaining.</returns>
        IClassSqlOptions<TEntity> OrderBy<TKey>(Expression<Func<TEntity, TKey>> keySelector);

        /// <summary>
        /// Adds an ORDER BY clause to the query with specified sort direction.
        /// </summary>
        /// <typeparam name="TKey">The type of the property to sort by.</typeparam>
        /// <param name="keySelector">Expression selecting the property to sort by.</param>
        /// <param name="asc">True for ascending order, false for descending.</param>
        /// <returns>The query builder instance for method chaining.</returns>
        IClassSqlOptions<TEntity> OrderBy<TKey>(Expression<Func<TEntity, TKey>> keySelector, bool asc);

        /// <summary>
        /// Adds a descending ORDER BY clause to the query.
        /// </summary>
        /// <typeparam name="TKey">The type of the property to sort by.</typeparam>
        /// <param name="keySelector">Expression selecting the property to sort by.</param>
        /// <returns>The query builder instance for method chaining.</returns>
        IClassSqlOptions<TEntity> OrderByDesc<TKey>(Expression<Func<TEntity, TKey>> keySelector);

        /// <summary>
        /// Adds an INNER JOIN clause to the query.
        /// </summary>
        /// <typeparam name="TJoin">The type of entity to join with.</typeparam>
        /// <typeparam name="TKey">The type of the join key.</typeparam>
        /// <param name="predicate">Expression defining the join condition.</param>
        /// <param name="alias">Alias for the joined table.</param>
        /// <returns>A join-specific query builder for continued configuration.</returns>
        IClassJoinSqlOptions<TEntity, TJoin> Join<TJoin, TKey>(Expression<Func<TEntity, TKey>> predicate, string alias)
            where TJoin : class;

        /// <summary>
        /// Adds a LEFT JOIN clause to the query.
        /// </summary>
        /// <typeparam name="TJoin">The type of entity to join with.</typeparam>
        /// <typeparam name="TKey">The type of the join key.</typeparam>
        /// <param name="predicate">Expression defining the join condition.</param>
        /// <param name="alias">Alias for the joined table.</param>
        /// <returns>A join-specific query builder for continued configuration.</returns>
        IClassJoinSqlOptions<TEntity, TJoin> JoinLeft<TJoin, TKey>(Expression<Func<TEntity, TKey>> predicate, string alias)
            where TJoin : class;

        IClassJoinSqlOptions<TEntity, TJoin> JoinRight<TJoin, TKey>(Expression<Func<TEntity, TKey>> predicate, string alias)
            where TJoin : class;

        IClassSqlOptions<TEntity> Offset(int offset);

        IClassSqlOptions<TEntity> Limit(int limit);

        IClassSqlOptions<TEntity> GroupBy<TKey>(Expression<Func<TEntity, TKey>> predicate);

        IClassSqlOptions<TEntity> Having<TKey>(Expression<Func<TEntity, TKey>> predicate);
    }
}
