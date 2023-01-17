using System.Collections.Generic;
using System.Threading.Tasks;

namespace ProjectName.Common
{
    public interface ISearchService
    {
        Task<IEnumerable<long>> SearchAsync(string sql, object param = null);

        Task<long> SearchByIdAsync(string sql, object param = null);
    }
}