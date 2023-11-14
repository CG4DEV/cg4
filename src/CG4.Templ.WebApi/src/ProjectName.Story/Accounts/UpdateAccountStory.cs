using System.Threading.Tasks;
using CG4.DataAccess;
using CG4.Executor.Story;
using ProjectName.Domain.Entities;
using ProjectName.Exceptions;

namespace ProjectName.Story.Accounts
{
    public class UpdateAccountStory : IStory<UpdateAccountStoryContext>
    {
        private readonly ICrudService _crudService;

        public UpdateAccountStory(ICrudService crudService)
        {
            _crudService = crudService;
        }

        public async Task ExecuteAsync(UpdateAccountStoryContext context)
        {
            var oldAccount = await _crudService.GetAsync<Account>(context.Id);

            if (oldAccount == null)
            {
                throw new NotFoundException();
            }

            if (oldAccount.Password != context.Data.OldPassword)
            {
                throw new ProjectNameException("Old password isn't match");
            }

            // TODO : swap to mapper
            oldAccount.Login = context.Data.Login;
            oldAccount.Name = context.Data.Name;
            oldAccount.Password = context.Data.Password;

            await _crudService.UpdateAsync(oldAccount);
        }
    }
}
