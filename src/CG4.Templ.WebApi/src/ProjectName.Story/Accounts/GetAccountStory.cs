using System.Threading.Tasks;
using CG4.DataAccess;
using CG4.Executor.Story;
using ProjectName.Contracts.Accounts;
using ProjectName.Domain.Entities;

namespace ProjectName.Story.Accounts
{
    public class GetAccountStory : IStory<GetAccountStoryContext, AccountDto>
    {
        private readonly ICrudService _crudService;

        public GetAccountStory(ICrudService crudService)
        {
            _crudService = crudService;
        }

        public async Task<AccountDto> ExecuteAsync(GetAccountStoryContext context)
        {
            var result = await _crudService.GetAsync<Account>(context.Id);

            if (result == null)
            {
                return null;
            }

            return new AccountDto
            {
                Id = result.Id,
                Name = result.Name,
                Login = result.Login,
            };
        }
    }
}
