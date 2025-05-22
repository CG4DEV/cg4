namespace CG4.DataAccess.Poco
{
    /// <summary>
    /// Base interface for SQL query building options.
    /// Provides core functionality needed by various SQL expression builders and query generators.
    /// </summary>
    public interface ISqlOption
    {
        /// <summary>
        /// Gets the alias used for the current table in SQL queries.
        /// This alias is used to qualify column names and avoid ambiguity in JOINs.
        /// </summary>
        string Alias { get; }

        /// <summary>
        /// Gets the .NET type that this SQL option is currently working with.
        /// Used for mapping properties and generating correct SQL expressions.
        /// </summary>
        /// <returns>The Type object representing the current entity type.</returns>
        Type GetCurrentType();
    }
}
