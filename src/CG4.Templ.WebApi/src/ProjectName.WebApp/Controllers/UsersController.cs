using System.Threading.Tasks;
using CG4.DataAccess.Domain;
using CG4.Executor.Story;
using CG4.Impl.Dapper.Crud;
using Microsoft.AspNetCore.Mvc;
using ProjectName.Core.Web.Controllers;
using ProjectName.Domain.Entities;
using ProjectName.Story.Users;

namespace ProjectName.WebApp.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UsersController : EntityControllerBase<User>
    {
        private readonly IStoryExecutor _storyExecutor;
        
        public UsersController(ICrudService crudService, IStoryExecutor storyExecutor) 
            : base(crudService)
        {
            _storyExecutor = storyExecutor;
        }
        
        [HttpGet("page")]
        public Task<PageResult<User>> GetPage([FromQuery] GetUsersPageStoryContext context)
        {
            return _storyExecutor.Execute(context);
        }
    }
}
