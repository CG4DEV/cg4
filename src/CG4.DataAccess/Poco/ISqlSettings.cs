namespace CG4.DataAccess.Poco
{
    /// <summary>
    /// Defines settings for SQL query generation specific to different SQL dialects.
    /// Provides configuration for SQL syntax elements like delimiters and parameter prefixes.
    /// </summary>
    public interface ISqlSettings
    {
        /// <summary>
        /// Gets the character used to start identifier delimitation (e.g., table names, column names).
        /// For example, " for standard SQL, [ for SQL Server, or ` for MySQL.
        /// </summary>
        char StartDelimiter { get; }

        /// <summary>
        /// Gets the character used to end identifier delimitation.
        /// Should pair with StartDelimiter (e.g., ", ], or `).
        /// </summary>
        char EndDelimiter { get; }

        /// <summary>
        /// Gets the prefix character used for SQL parameters.
        /// Common values are @ for SQL Server/PostgreSQL or : for Oracle.
        /// </summary>
        char ParameterPrefix { get; }

        /// <summary>
        /// Gets the separator used between SQL batches.
        /// Typically a semicolon followed by a newline.
        /// </summary>
        string BatchSeperator { get; }

        /// <summary>
        /// Gets whether the database system uses schemas.
        /// Affects how fully qualified object names are generated.
        /// </summary>
        bool IsUsingSchemas { get; }
    }
}
