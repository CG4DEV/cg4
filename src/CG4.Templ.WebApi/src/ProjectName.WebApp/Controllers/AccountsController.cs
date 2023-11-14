using System.Collections.Generic;
using System.Threading.Tasks;
using CG4.DataAccess.Domain;
using CG4.Executor.Story;
using Microsoft.AspNetCore.Mvc;
using ProjectName.Contracts;
using ProjectName.Contracts.Accounts;
using ProjectName.Story.Accounts;

namespace ProjectName.WebApp.Controllers
{
    [Route("api/[controller]")]
    public class AccountsController : ProjectNameControllerBase
    {
        private readonly IStoryExecutor _storyExecutor;

        public AccountsController(IStoryExecutor storyExecutor)
        {
            _storyExecutor = storyExecutor;
        }

        [HttpGet]
        public Task<IEnumerable<AccountDto>> GetList([FromQuery] int? limit, [FromQuery] AccountQueryDto query)
        {
            return _storyExecutor.Execute(new GetAccountsListStoryContext { Limit = limit, Query = query });
        }

        [HttpGet("{id:long}")]
        [ProducesResponseType(typeof(AccountDto), 200)]
        [ProducesResponseType(typeof(ErrorDetails), 404)]
        public Task<AccountDto> Get(long id)
        {
            return _storyExecutor.Execute(new GetAccountStoryContext { Id = id });
        }

        [HttpPost]
        public Task<CreatedIdResult> Create([FromBody] AccountCreateDto account)
        {
            return _storyExecutor.Execute(new CreateAccountStoryContext { Data = account });
        }

        [HttpPut("{id:long}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(typeof(ErrorDetails), 400)]
        [ProducesResponseType(typeof(ErrorDetails), 404)]
        public Task Update([FromRoute] long id, [FromBody] AccountUpdateDto account)
        {
            return _storyExecutor.Execute(new UpdateAccountStoryContext { Id = id, Data = account });
        }

        [HttpDelete("{id:long}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(typeof(ErrorDetails), 404)]
        public Task Delete(long id)
        {
            return _storyExecutor.Execute(new DeleteAccountStoryContext { Id = id });
        }

        [HttpGet("page")]
        public Task<PageResult<AccountDto>> GetPage([FromQuery] int? limit, [FromQuery] int? page, [FromQuery] AccountQueryDto query)
        {
            return _storyExecutor.Execute(new GetAccountsPageStoryContext { Limit = limit, Page = page, Query = query });
        }
    }
}
