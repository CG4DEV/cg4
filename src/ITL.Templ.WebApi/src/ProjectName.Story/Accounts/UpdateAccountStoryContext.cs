using ITL.Executor;
using ProjectName.Contracts.Accounts;

namespace ProjectName.Story.Accounts
{
    public class UpdateAccountStoryContext : IResult
    {
        public long Id { get; set; }

        public AccountUpdateDto Data { get; set; }
    }
}
