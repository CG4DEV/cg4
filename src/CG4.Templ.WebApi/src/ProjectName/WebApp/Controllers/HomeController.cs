using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace ProjectName.WebApp.Controllers
{
    [ApiExplorerSettings(IgnoreApi = true)]
    public class HomeController : Controller
    {
        /// <summary>
        /// Get version app from file 'appversion.txt'.
        /// </summary>
        /// <returns></returns>
        [Route("/version")]
        [HttpGet]
        public async Task<string> GetVersion()
        {
            const string versionFile = "appversion.txt";

            if (System.IO.File.Exists(versionFile))
            {
                return await System.IO.File.ReadAllTextAsync(versionFile);
            }

            return null;
        }
    }
}