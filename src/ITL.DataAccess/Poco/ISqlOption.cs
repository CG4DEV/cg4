namespace ITL.DataAccess.Poco
{
    public interface ISqlOption
    {
        string Alias { get; }

        Type GetCurrentType();
    }
}
