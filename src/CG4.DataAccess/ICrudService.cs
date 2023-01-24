using CG4.DataAccess;

namespace CG4.DataAccess
{
    /// <summary>
    /// CRUD-сервис.
    /// </summary>
    public interface ICrudService : IAppCrudService, ISqlRepository, ISqlRepositoryAsync
    {
    }
}
