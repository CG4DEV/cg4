using ITL.Executor;
using ProjectName.Contracts;
using ProjectName.Contracts.Accounts;

namespace ProjectName.Story.Accounts
{
    public class CreateAccountStoryContext : IResult<CreatedIdResult>
    {
        public AccountCreateDto Data { get; set; }
    }
}
