using System.Threading.Tasks;
using ITL.DataAccess;
using ITL.Executor.Story;
using ProjectName.Domain.Entities;
using ProjectName.Exceptions;

namespace ProjectName.Story.Accounts
{
    public class DeleteAccountStory : IStory<DeleteAccountStoryContext>
    {
        private readonly ICrudService _crudService;

        public DeleteAccountStory(ICrudService crudService)
        {
            _crudService = crudService;
        }

        public async Task ExecuteAsync(DeleteAccountStoryContext context)
        {
            var account = await _crudService.GetAsync<Account>(context.Id);

            if (account == null)
            {
                throw new NotFoundException();
            }

            await _crudService.DeleteAsync<Account>(context.Id);
        }
    }
}
