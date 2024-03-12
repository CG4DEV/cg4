using System.Collections.Generic;
using ITL.Executor;
using ProjectName.Contracts.Accounts;

namespace ProjectName.Story.Accounts
{
    public class GetAccountsListStoryContext : IResult<IEnumerable<AccountDto>>
    {
        public int? Limit { get; set; } = 25;

        public AccountQueryDto Query { get; set; }
    }
}
