using System.Data;
using System.Threading.Tasks;
using CG4.DataAccess.Domain;

namespace ProjectName.Core.DataAccess
{
    public interface IDataService
    {
        Task<T> CreateAsync<T>(T entity, IDbConnection connection = null, IDbTransaction transaction = null) 
            where T : EntityBase, new();

        Task<T> UpdateAsync<T>(T entity, IDbConnection connection = null, IDbTransaction transaction = null) 
            where T : EntityBase, new();
    }
}