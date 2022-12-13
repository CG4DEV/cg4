using Microsoft.Extensions.Configuration;
using ProjectName.Common;

namespace ProjectName.WebApp
{
    public interface IAppSettings
    {
    }

    public class AppSettings : IAppSettings, IConnectionSettings
    {
        public AppSettings(IConfiguration config)
        {
            ConnectionString = config.GetConnectionString("ProjectName");
        }

        public string ConnectionString { get; set; }
    }
}
