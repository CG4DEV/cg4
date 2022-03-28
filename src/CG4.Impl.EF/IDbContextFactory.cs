using System.Data.Common;
using Microsoft.EntityFrameworkCore;

namespace CG4.Impl.EF
{
    public interface IDbContextFactory
    {
        DbContext CreateContext(DbConnection dbConnection = null);
    }
}
