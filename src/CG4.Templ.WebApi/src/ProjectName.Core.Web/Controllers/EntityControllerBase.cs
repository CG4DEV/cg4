using System.Threading.Tasks;
using CG4.DataAccess.Domain;
using CG4.Impl.Dapper.Crud;
using CG4.Impl.Dapper.Poco;
using CG4.Impl.Dapper.Poco.Expressions;
using Microsoft.AspNetCore.Mvc;
using ProjectName.Core.DataAccess;

namespace ProjectName.Core.Web.Controllers
{
    public class EntityControllerBase<TEntity> : ProjectNameControllerBase
        where TEntity : EntityBase, new()
    {
        protected readonly ICrudService _crudService;
        protected readonly IDataService _dataService;

        public EntityControllerBase(ICrudService crudService, IDataService dataService)
        {
            _crudService = crudService;
            _dataService = dataService;
        }

        [HttpGet]
        public virtual async Task<IActionResult> GetList([FromQuery] int? limit, [FromQuery] string? search)
        {
            var expr = ExprBoolean.Empty;

            if (!string.IsNullOrEmpty(search) && int.TryParse(search, out var id))
            {
                expr |= SqlExprHelper.GenerateWhere<TEntity>(u => u.Id == id);
            }

            var result = await _crudService.GetAllAsync<TEntity>(x => x
                .Where(expr).Limit(limit ?? 25));

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
            var result = await _dataService.CreateAsync(entity);
            return Ok(result);
        }

        [HttpPut]
        public virtual async Task<IActionResult> Update(TEntity entity)
        {
            var result = await _dataService.UpdateAsync(entity);
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