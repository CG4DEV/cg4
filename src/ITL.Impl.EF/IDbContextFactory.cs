using System.Data.Common;
using Microsoft.EntityFrameworkCore;

namespace ITL.Impl.EF
{
    public interface IDbContextFactory
    {
        DbContext CreateContext(DbConnection dbConnection = null);
    }
}
