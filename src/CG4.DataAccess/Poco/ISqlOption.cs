namespace CG4.DataAccess.Poco
{
    public interface ISqlOption
    {
        string Alias { get; }

        Type GetCurrentType();
    }
}
