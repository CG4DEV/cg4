using System.Threading.Tasks;
using CG4.DataAccess;
using CG4.DataAccess.Domain;
using Microsoft.AspNetCore.Mvc;
using ProjectName.WebApp.Attributes;

namespace ProjectName.WebApp.Controllers
{
    [ApiController]
    [KebabCaseNaming]
    [ApiExplorerSettings()]
    [Route("[controller]")]
    public class EntityControllerBase<TEntity> : Controller
        where TEntity : EntityBase, new()
    {
        protected readonly ICrudService _crudService;

        public EntityControllerBase(ICrudService crudService)
        {
            _crudService = crudService;
        }

        [HttpGet]
        public virtual async Task<IActionResult> GetList([FromQuery] int? limit)
        {
            var result = await _crudService.GetAllAsync<TEntity>(x => x.Limit(limit ?? 25));
            return Ok(result);
        }

        [HttpGet("{id:long}")]
        public virtual async Task<IActionResult> Get(long id)
        {
            var result = await _crudService.GetAsync<TEntity>(id);
            return Ok(result);
        }

        [HttpPost]
        public virtual async Task<IActionResult> Create(TEntity entity)
        {
            var result = await _crudService.CreateAsync(entity);
            return Ok(result);
        }

        [HttpPut]
        public virtual async Task<IActionResult> Update(TEntity entity)
        {
            var result = await _crudService.UpdateAsync(entity);
            return Ok(result);
        }

        [HttpDelete("{id}")]
        public virtual async Task<IActionResult> Delete(long id)
        {
            await _crudService.DeleteAsync<TEntity>(id);
            return Ok();
        }
    }
}