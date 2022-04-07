namespace CG4.Impl.Dapper.Poco
{
    public interface ISqlSettings
    {
        char StartDelimiter { get; }

        char EndDelimiter { get; }

        char ParameterPrefix { get; }

        string BatchSeperator { get; }

        bool IsUsingSchemas { get; }
    }
}