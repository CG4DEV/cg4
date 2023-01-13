using System.Collections.Generic;
using System.Threading.Tasks;

namespace ProjectName.Common
{
    public interface ISphinxService
    {
        string EscapeForExtendedSearch(string input);

        Task<IEnumerable<TEntity>> SearchAsync<TEntity>(string sql, object param = null);

        Task<TEntity> SearchByIdAsync<TEntity>(string sql, object param = null);
    }
}