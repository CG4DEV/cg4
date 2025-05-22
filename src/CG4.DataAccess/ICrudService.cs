namespace CG4.DataAccess
{
    /// <summary>
    /// Service for performing CRUD (Create, Read, Update, Delete) operations.
    /// Combines functionality from IAppCrudService and ISqlRepositoryAsync interfaces.
    /// </summary>
    public interface ICrudService : IAppCrudService, ISqlRepositoryAsync
    {
    }
}
