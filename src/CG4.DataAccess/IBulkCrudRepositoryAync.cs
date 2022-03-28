using System.Data;

namespace CG4.DataAccess
{
    public interface IBulkCrudRepositoryAync
    {
        public Task<IEnumerable<T>> DeleteAsync<T>(IEnumerable<T> entities, IDbConnection connection = null, IDbTransaction transaction = null)
            where T : class;

        public Task<IEnumerable<T>> InsertAsync<T>(IEnumerable<T> entities, IDbConnection connection = null, IDbTransaction transaction = null)
            where T : class;

        public Task<IEnumerable<T>> UpdateAsync<T>(IEnumerable<T> entities, IDbConnection connection = null, IDbTransaction transaction = null)
            where T : class;
    }
}
