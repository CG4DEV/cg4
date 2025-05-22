using System;

namespace CG4.DataAccess.Poco
{
    /// <summary>
    /// Default implementation of ISqlSettings that provides ANSI SQL standard settings.
    /// Can be used as a base class for dialect-specific settings implementations.
    /// </summary>
    public class SqlSettings : ISqlSettings
    {
        /// <summary>
        /// Initializes a new instance of the SqlSettings class with ANSI SQL standard defaults:
        /// - Double quotes (") for identifier delimitation
        /// - @ for parameter prefixes
        /// - Semicolon + newline for batch separation
        /// </summary>
        public SqlSettings()
        {
            // The ANSI standard delimeter is a double quote mark
            StartDelimiter = '"';
            EndDelimiter = '"';
            ParameterPrefix = '@';
            BatchSeperator = $";{Environment.NewLine}";
        }

        /// <summary>
        /// Gets or sets the character used for starting identifier delimitation.
        /// Defaults to double quote (") for ANSI SQL compliance.
        /// </summary>
        public char StartDelimiter { get; protected set; }

        /// <summary>
        /// Gets or sets the character used for ending identifier delimitation.
        /// Defaults to double quote (") for ANSI SQL compliance.
        /// </summary>
        public char EndDelimiter { get; protected set; }

        /// <summary>
        /// Gets or sets the prefix character used for SQL parameters.
        /// Defaults to @ which is compatible with SQL Server and PostgreSQL.
        /// </summary>
        public char ParameterPrefix { get; protected set; }

        /// <summary>
        /// Gets or sets the separator used between SQL batches.
        /// Defaults to semicolon followed by a newline.
        /// </summary>
        public string BatchSeperator { get; protected set; }

        /// <summary>
        /// Gets or sets whether the database system uses schemas.
        /// Defaults to false in the base SqlSettings class.
        /// </summary>
        public bool IsUsingSchemas { get; protected set; }
    }

    /// <summary>
    /// SQL Server (Microsoft SQL Server) specific SQL settings.
    /// Configures square bracket delimiters and schema support.
    /// </summary>
    public class MsSqlOptions : SqlSettings
    {
        /// <summary>
        /// Initializes SQL Server specific settings:
        /// - Square brackets ([]) for identifier delimitation
        /// - Enables schema support
        /// </summary>
        public MsSqlOptions()
        {
            StartDelimiter = '[';
            EndDelimiter = ']';
            IsUsingSchemas = true;
        }
    }

    /// <summary>
    /// MySQL specific SQL settings.
    /// Configures backtick delimiters and disables schema support.
    /// </summary>
    public class MySqlOptions : SqlSettings
    {
        /// <summary>
        /// Initializes MySQL specific settings:
        /// - Backticks (`) for identifier delimitation
        /// - Disables schema support (MySQL uses databases instead)
        /// </summary>
        public MySqlOptions()
        {
            StartDelimiter = EndDelimiter = '`';
        }
    }

    /// <summary>
    /// PostgreSQL specific SQL settings.
    /// Uses standard ANSI SQL delimiters with schema support.
    /// </summary>
    public class PostgreSqlOptions : SqlSettings
    {
        /// <summary>
        /// Initializes PostgreSQL specific settings:
        /// - Keeps default ANSI SQL double quotes for identifiers
        /// - Enables schema support
        /// </summary>
        public PostgreSqlOptions()
        {
            IsUsingSchemas = true;
        }
    }
}
