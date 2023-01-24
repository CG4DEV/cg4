using Microsoft.AspNetCore.Mvc;
using ProjectName.Core.Web.Attributes;

namespace ProjectName.Core.Web.Controllers
{
    [ApiController]
    [KebabCaseNaming]
    [ApiExplorerSettings()]
    public class ProjectNameControllerBase : Controller
    {
        
    }
}