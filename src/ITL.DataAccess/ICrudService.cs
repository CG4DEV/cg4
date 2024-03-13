namespace ITL.DataAccess
{
    /// <summary>
    /// CRUD-сервис.
    /// </summary>
    public interface ICrudService : IAppCrudService, ISqlRepository, ISqlRepositoryAsync
    {
    }
}
