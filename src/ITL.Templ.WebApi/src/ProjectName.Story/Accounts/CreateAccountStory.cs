using System.Threading.Tasks;
using ITL.DataAccess;
using ITL.Executor.Story;
using ProjectName.Contracts;
using ProjectName.Domain.Entities;

namespace ProjectName.Story.Accounts
{
    public class CreateAccountStory : IStory<CreateAccountStoryContext, CreatedIdResult>
    {
        private readonly ICrudService _crudService;

        public CreateAccountStory(ICrudService crudService)
        {
            _crudService = crudService;
        }

        public async Task<CreatedIdResult> ExecuteAsync(CreateAccountStoryContext context)
        {
            var account = new Account
            {
                Login = context.Data.Login,
                Name = context.Data.Name,
                Password = context.Data.Password,
            };

            var result = await _crudService.CreateAsync(account);

            return new CreatedIdResult { Id = result.Id };
        }
    }
}
