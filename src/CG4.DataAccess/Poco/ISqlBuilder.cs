using System.Linq.Expressions;
using CG4.DataAccess.Poco.Expressions;

namespace CG4.DataAccess.Poco
{
    /// <summary>
    /// Provides an interface for building SQL queries using a fluent API.
    /// Supports common SQL operations like SELECT, INSERT, UPDATE, and DELETE.
    /// </summary>
    public interface ISqlBuilder
    {
        /// <summary>
        /// Generates an INSERT SQL statement for the specified entity type.
        /// </summary>
        /// <typeparam name="T">The entity type to generate the INSERT statement for.</typeparam>
        /// <returns>A parameterized INSERT SQL statement with RETURNING clause.</returns>
        string Insert<T>() where T : class;

        /// <summary>
        /// Generates an UPDATE SQL statement for the specified entity type, filtering by ID.
        /// </summary>
        /// <typeparam name="T">The entity type to generate the UPDATE statement for.</typeparam>
        /// <returns>A parameterized UPDATE SQL statement.</returns>
        string UpdateById<T>() where T : class;

        /// <summary>
        /// Generates a DELETE SQL statement for the specified entity type, filtering by ID.
        /// </summary>
        /// <typeparam name="T">The entity type to generate the DELETE statement for.</typeparam>
        /// <returns>A parameterized DELETE SQL statement.</returns>
        string DeleteById<T>() where T : class;

        /// <summary>
        /// Generates a SELECT SQL statement to retrieve a single entity by its ID.
        /// </summary>
        /// <typeparam name="T">The entity type to generate the SELECT statement for.</typeparam>
        /// <param name="predicate">Optional expression to further customize the query.</param>
        /// <returns>A parameterized SELECT SQL statement.</returns>
        string GetById<T>(Expression<Action<IClassSqlOptions<T>>> predicate = null) where T : class;

        /// <summary>
        /// Generates a SELECT SQL statement to retrieve all entities of the specified type.
        /// </summary>
        /// <typeparam name="T">The entity type to generate the SELECT statement for.</typeparam>
        /// <param name="predicate">Optional expression to customize the query with WHERE, ORDER BY, etc.</param>
        /// <returns>A parameterized SELECT SQL statement.</returns>
        string GetAll<T>(Expression<Action<IClassSqlOptions<T>>> predicate = null) where T : class;

        /// <summary>
        /// Generates a SELECT COUNT(*) SQL statement for the specified entity type.
        /// </summary>
        /// <typeparam name="T">The entity type to count.</typeparam>
        /// <param name="predicate">Optional expression to filter the counted entities.</param>
        /// <returns>A parameterized SELECT COUNT(*) SQL statement.</returns>
        string Count<T>(Expression<Action<IClassSqlOptions<T>>> predicate = null) where T : class;

        /// <summary>
        /// Generates an expression tree representing a SQL query.
        /// </summary>
        /// <typeparam name="T">The entity type to build the query for.</typeparam>
        /// <param name="predicate">Optional expression to customize the query.</param>
        /// <returns>An ExprSql object representing the query structure.</returns>
        ExprSql GenerateSql<T>(Expression<Action<IClassSqlOptions<T>>> predicate = null) where T : class;

        /// <summary>
        /// Serializes an expression tree into a SQL string.
        /// </summary>
        /// <param name="sql">The expression tree to serialize.</param>
        /// <returns>A SQL string representing the expression tree.</returns>
        string Serialize(ExprSql sql);
    }
}
