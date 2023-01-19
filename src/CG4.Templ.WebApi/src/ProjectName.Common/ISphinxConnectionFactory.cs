using System.Data;
using System.Threading.Tasks;

namespace ProjectName.Common
{
    public interface ISphinxConnectionFactory
    {
        IDbConnection Create(string connectionString = null);

        Task<IDbConnection> CreateAsync(string connectionString = null);
    }
}