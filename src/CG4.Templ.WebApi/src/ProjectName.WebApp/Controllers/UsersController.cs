using CG4.Impl.Dapper.Crud;
using Microsoft.AspNetCore.Mvc;
using ProjectName.Core.DataAccess;
using ProjectName.Core.Web.Controllers;
using ProjectName.Domain.Entities;

namespace ProjectName.WebApp.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UsersController : EntityControllerBase<User>
    {
        public UsersController(ICrudService crudService, IDataService dataService) 
            : base(crudService, dataService)
        {
            
        }
    }
}
