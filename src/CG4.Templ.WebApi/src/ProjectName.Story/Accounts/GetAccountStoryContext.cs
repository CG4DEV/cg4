using CG4.Executor;
using ProjectName.Contracts.Accounts;

namespace ProjectName.Story.Accounts
{
    public class GetAccountStoryContext : IResult<AccountDto>
    {
        public long Id { get; set; }
    }
}
