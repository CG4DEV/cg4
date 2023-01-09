using System;
using System.Data;
using System.Threading.Tasks;
using CG4.DataAccess.Domain;
using CG4.Impl.Dapper.Crud;

namespace ProjectName.Core.DataAccess
{
    public class DataService : IDataService
    {
        private readonly ICrudService _crudService;
        
        public DataService(ICrudService crudService)
        {
            _crudService = crudService;
        }
        
        public Task<T> CreateAsync<T>(T entity, IDbConnection connection = null, IDbTransaction transaction = null) 
            where T : EntityBase, new()
        {
            entity.CreateDate = DateTimeOffset.UtcNow;
            entity.UpdateDate = DateTimeOffset.UtcNow;

            return _crudService.CreateAsync(entity, connection, transaction);
        }

        public Task<T> UpdateAsync<T>(T entity, IDbConnection connection = null, IDbTransaction transaction = null) 
            where T : EntityBase, new()
        {
            entity.UpdateDate = DateTimeOffset.UtcNow;

            return _crudService.UpdateAsync(entity, connection, transaction);
        }
    }
}