using Microsoft.AspNetCore.Mvc;
using ProjectName.WebApp.Attributes;

namespace ProjectName.WebApp.Controllers
{
    [ApiController]
    [KebabCaseNaming]
    [ApiExplorerSettings]
    public class ProjectNameControllerBase : ControllerBase
    {
    }
}
