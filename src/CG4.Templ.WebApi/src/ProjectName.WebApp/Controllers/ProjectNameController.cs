using System.Collections.Generic;
using System.Threading.Tasks;
using CG4.Executor.Story;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProjectName.Domain.Entities;
using ProjectName.Story.Users;

namespace ProjectName.WebApp.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProjectNameController : ControllerBase
    {
        private readonly IStoryExecutor _storyExecutor;

        public ProjectNameController(IStoryExecutor storyExecutor)
        {
            _storyExecutor = storyExecutor;
        }

        [HttpGet("test")]
        [ProducesResponseType(typeof(IEnumerable<User>), StatusCodes.Status200OK)]
        public Task<IEnumerable<User>> GetEntities()
        {
            return _storyExecutor.Execute(new GetListUserStoryContext());
        }
    }
}
