using CG4.DataAccess;

namespace CG4.Impl.Dapper.Crud
{
    public interface ICrudService : IAppCrudService, ISqlRepository, ISqlRepositoryAsync, ISqlCrudRepository, ISqlCrudRepositoryAsync
    {
    }
}
