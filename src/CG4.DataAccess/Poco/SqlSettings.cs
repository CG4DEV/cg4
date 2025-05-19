namespace CG4.DataAccess.Poco
{
    /// <summary>
    /// Stores database options linked to a particular dialect.
    /// </summary>
    public class SqlSettings : ISqlSettings
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SqlSettings"/> class.
        /// Default constructor
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
        /// Gets the start delimiter used for SQL identifiers.
        /// </summary>
        public char StartDelimiter { get; protected set; }

        /// <summary>
        /// Gets the end delimiter used for SQL identifiers.
        /// </summary>
        public char EndDelimiter { get; protected set; }

        /// <summary>
        /// Gets the prefix used for named parameters
        /// </summary>
        public char ParameterPrefix { get; protected set; }

        public string BatchSeperator { get; protected set; }

        /// <summary>
        /// Gets a flag indicating the database is using schemas.
        /// </summary>
        public bool IsUsingSchemas { get; protected set; }
    }

    public class MsSqlOptions : SqlSettings
    {
        public MsSqlOptions()
        {
            StartDelimiter = '[';
            EndDelimiter = ']';
            IsUsingSchemas = true;
        }
    }

    public class MySqlOptions : SqlSettings
    {
        public MySqlOptions()
        {
            StartDelimiter = EndDelimiter = '`';
        }
    }

    public class PostgreSqlOptions : SqlSettings
    {
        public PostgreSqlOptions()
        {
            IsUsingSchemas = true;
        }
    }
}
