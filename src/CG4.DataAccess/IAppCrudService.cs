using System.Data;
using System.Linq.Expressions;
using CG4.DataAccess.Domain;
using CG4.DataAccess.Poco;

namespace CG4.DataAccess
{
    /// <summary>
    /// Core CRUD service interface for working with data sources.
    /// Provides high-level access to database operations with support for transactions and typed entities.
    /// </summary>
    public interface IAppCrudService
    {
        /// <summary>
        /// Asynchronously retrieves an entity by its unique identifier.
        /// </summary>
        /// <param name="id">The unique identifier of the entity.</param>
        /// <param name="connection">An open connection to the data source.</param>
        /// <param name="transaction">The transaction to be used for the operation.</param>
        /// <typeparam name="T">The type of entity to retrieve. Must implement <see cref="IEntityBase"/>.</typeparam>
        /// <returns>An entity of type T or null if not found.</returns>
        Task<T?> GetAsync<T>(long id, IDbConnection? connection = null, IDbTransaction? transaction = null)
            where T : class, IEntityBase, new();

        /// <summary>
        /// Asynchronously retrieves an entity based on a query expression.
        /// </summary>
        /// <param name="predicate">Expression of type <see cref="IClassSqlOptions{TEntity}"/> for query building.</param>
        /// <param name="connection">An open connection to the data source.</param>
        /// <param name="transaction">The transaction to be used for the operation.</param>
        /// <typeparam name="T">The type of entity to retrieve. Must implement <see cref="IEntityBase"/>.</typeparam>
        /// <returns>An entity of type T or null if not found.</returns>
        Task<T?> GetAsync<T>(Expression<Action<IClassSqlOptions<T>>> predicate, IDbConnection? connection = null, IDbTransaction? transaction = null)
            where T : class, IEntityBase, new();

        /// <summary>
        /// Asynchronously retrieves an entity by its unique identifier and converts it to another type.
        /// </summary>
        /// <param name="id">The unique identifier of the entity.</param>
        /// <param name="connection">An open connection to the data source.</param>
        /// <param name="transaction">The transaction to be used for the operation.</param>
        /// <typeparam name="TEntity">The source entity type. Must implement <see cref="IEntityBase"/>.</typeparam>
        /// <typeparam name="TResult">The target type to convert the entity to.</typeparam>
        /// <returns>A converted entity of type TResult or null if not found.</returns>
        Task<TResult?> GetAsync<TEntity, TResult>(long id, IDbConnection? connection = null, IDbTransaction? transaction = null)
            where TEntity : class, IEntityBase, new()
            where TResult : class, new();

        /// <summary>
        /// Asynchronously retrieves an entity based on a query expression and converts it to another type.
        /// </summary>
        /// <param name="predicate">Expression of type <see cref="IClassSqlOptions{TEntity}"/> for query building.</param>
        /// <param name="connection">An open connection to the data source.</param>
        /// <param name="transaction">The transaction to be used for the operation.</param>
        /// <typeparam name="TEntity">The source entity type. Must implement <see cref="IEntityBase"/>.</typeparam>
        /// <typeparam name="TResult">The target type to convert the entity to.</typeparam>
        /// <returns>A converted entity of type TResult or null if not found.</returns>
        Task<TResult?> GetAsync<TEntity, TResult>(Expression<Action<IClassSqlOptions<TEntity>>> predicate, IDbConnection? connection = null, IDbTransaction? transaction = null)
            where TEntity : class, IEntityBase, new()
            where TResult : class, new();
            
        /// <summary>
        /// Asynchronously retrieves a list of entities based on a query expression.
        /// </summary>
        /// <param name="predicate">Optional expression of type <see cref="IClassSqlOptions{TEntity}"/> for query building.</param>
        /// <param name="connection">An open connection to the data source.</param>
        /// <param name="transaction">The transaction to be used for the operation.</param>
        /// <typeparam name="T">The type of entities to retrieve. Must implement <see cref="IEntityBase"/>.</typeparam>
        /// <returns>A list of entities of type T.</returns>
        Task<IEnumerable<T>> GetAllAsync<T>(Expression<Action<IClassSqlOptions<T>>>? predicate = null, IDbConnection? connection = null, IDbTransaction? transaction = null)
            where T : class, IEntityBase, new();

        /// <summary>
        /// Asynchronously retrieves a list of entities based on a query expression and converts them to another type.
        /// </summary>
        /// <param name="predicate">Optional expression of type <see cref="IClassSqlOptions{TEntity}"/> for query building.</param>
        /// <param name="connection">An open connection to the data source.</param>
        /// <param name="transaction">The transaction to be used for the operation.</param>
        /// <typeparam name="TEntity">The source entity type. Must implement <see cref="IEntityBase"/>.</typeparam>
        /// <typeparam name="TResult">The target type to convert the entities to.</typeparam>
        /// <returns>A list of converted entities of type TResult.</returns>
        Task<IEnumerable<TResult>> GetAllAsync<TEntity, TResult>(Expression<Action<IClassSqlOptions<TEntity>>>? predicate = null, IDbConnection? connection = null, IDbTransaction? transaction = null)
            where TEntity : class, IEntityBase, new()
            where TResult : class, new();

        /// <summary>
        /// Asynchronously creates a new entity in the data source.
        /// </summary>
        /// <param name="entity">The entity to create.</param>
        /// <param name="connection">An open connection to the data source.</param>
        /// <param name="transaction">The transaction to be used for the operation.</param>
        /// <typeparam name="T">The type of entity to create. Must implement <see cref="IEntityBase"/>.</typeparam>
        /// <returns>The created entity with its generated ID.</returns>
        Task<T> CreateAsync<T>(T entity, IDbConnection? connection = null, IDbTransaction? transaction = null)
            where T : class, IEntityBase, new();

        /// <summary>
        /// Asynchronously updates an existing entity in the data source.
        /// </summary>
        /// <param name="entity">The entity to update.</param>
        /// <param name="connection">An open connection to the data source.</param>
        /// <param name="transaction">The transaction to be used for the operation.</param>
        /// <typeparam name="T">The type of entity to update. Must implement <see cref="IEntityBase"/>.</typeparam>
        /// <returns>The updated entity.</returns>
        Task<T> UpdateAsync<T>(T entity, IDbConnection? connection = null, IDbTransaction? transaction = null)
            where T : class, IEntityBase, new();

        /// <summary>
        /// Asynchronously deletes an entity from the data source.
        /// </summary>
        /// <param name="entity">The entity to delete.</param>
        /// <param name="connection">An open connection to the data source.</param>
        /// <param name="transaction">The transaction to be used for the operation.</param>
        /// <typeparam name="T">The type of entity to delete. Must implement <see cref="IEntityBase"/>.</typeparam>
        Task DeleteAsync<T>(T entity, IDbConnection? connection = null, IDbTransaction? transaction = null)
            where T : class, IEntityBase, new();

        /// <summary>
        /// Asynchronously deletes an entity by its unique identifier.
        /// </summary>
        /// <param name="id">The unique identifier of the entity to delete.</param>
        /// <param name="connection">An open connection to the data source.</param>
        /// <param name="transaction">The transaction to be used for the operation.</param>
        /// <typeparam name="T">The type of entity to delete. Must implement <see cref="IEntityBase"/>.</typeparam>
        Task DeleteAsync<T>(long id, IDbConnection? connection = null, IDbTransaction? transaction = null)
            where T : class, IEntityBase, new();

        /// <summary>
        /// Asynchronously retrieves a paginated list of entities based on a query expression.
        /// </summary>
        /// <param name="page">The page number (1-based).</param>
        /// <param name="take">The number of entities per page.</param>
        /// <param name="predicate">Optional expression of type <see cref="IClassSqlOptions{TEntity}"/> for query building.</param>
        /// <param name="connection">An open connection to the data source.</param>
        /// <param name="transaction">The transaction to be used for the operation.</param>
        /// <typeparam name="TEntity">The type of entities to retrieve. Must implement <see cref="IEntityBase"/>.</typeparam>
        /// <returns>A page of entities with total count information.</returns>
        Task<PageResult<TEntity>> GetPageAsync<TEntity>(int? page, int? take, Expression<Action<IClassSqlOptions<TEntity>>>? predicate = null, IDbConnection? connection = null, IDbTransaction? transaction = null)
            where TEntity : class, IEntityBase, new();

        /// <summary>
        /// Asynchronously retrieves a paginated list of entities based on a query expression and converts them to another type.
        /// </summary>
        /// <param name="page">The page number (1-based).</param>
        /// <param name="take">The number of entities per page.</param>
        /// <param name="predicate">Optional expression of type <see cref="IClassSqlOptions{TEntity}"/> for query building.</param>
        /// <param name="connection">An open connection to the data source.</param>
        /// <param name="transaction">The transaction to be used for the operation.</param>
        /// <typeparam name="TEntity">The source entity type. Must implement <see cref="IEntityBase"/>.</typeparam>
        /// <typeparam name="TResult">The target type to convert the entities to.</typeparam>
        /// <returns>A page of converted entities with total count information.</returns>
        Task<PageResult<TResult>> GetPageAsync<TEntity, TResult>(int? page, int? take, Expression<Action<IClassSqlOptions<TEntity>>>? predicate = null, IDbConnection? connection = null, IDbTransaction? transaction = null)
            where TEntity : class, IEntityBase, new()
            where TResult : class, new();
    }
}
