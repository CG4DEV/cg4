using System;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace ProjectName.Core.Web.Controllers
{
    [ApiExplorerSettings(IgnoreApi = true)]
    public class HomeController : ControllerBase
    {
        private static readonly DateTimeOffset _starts = DateTimeOffset.UtcNow; 
        
        /// <summary>
        /// Service base page.
        /// </summary>
        [Route("/")]
        [HttpGet]
        public IActionResult GetIndex()
        {
            var sb = new StringBuilder();

            sb.AppendLine("ProjectName web");
            sb.AppendLine("</br></br>");
            sb.AppendLine("<a href='version'>/version</a></br>");
            sb.AppendLine("<a href='swagger'>/swagger</a></br>");
            sb.AppendLine("<a href='health'>/health</a></br>");
            sb.AppendLine("<a href='ready'>/ready</a></br>");
            sb.AppendLine("<a href='starts'>/starts</a></br>");

            return Content(sb.ToString(), "text/html");
        }

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

        /// <summary>
        /// Service start date.
        /// </summary>
        [Route("/starts")]
        [HttpGet]
        public DateTimeOffset Starts()
        {
            return _starts;
        }
    }
}