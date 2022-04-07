namespace CG4.Impl.Dapper.Poco
{
    public interface ISqlOption
    {
        string Alias { get; }
        Type GetCurrentType();
    }
}
