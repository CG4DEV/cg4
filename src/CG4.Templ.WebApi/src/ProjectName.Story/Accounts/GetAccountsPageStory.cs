using System.Threading.Tasks;
using CG4.DataAccess;
using CG4.DataAccess.Domain;
using CG4.DataAccess.Poco;
using CG4.DataAccess.Poco.Expressions;
using CG4.Executor.Story;
using ProjectName.Contracts.Accounts;
using ProjectName.Domain.Entities;

namespace ProjectName.Story.Accounts
{
    public class GetAccountsPageStory : IStory<GetAccountsPageStoryContext, PageResult<AccountDto>>
    {
        private readonly ICrudService _crudService;

        public GetAccountsPageStory(ICrudService crudService)
        {
            _crudService = crudService;
        }

        public Task<PageResult<AccountDto>> ExecuteAsync(GetAccountsPageStoryContext context)
        {
            var limit = context.Limit ?? 25;
            var page = context.Page ?? 0;

            var expr = ExprBoolean.Empty;

            if (!string.IsNullOrEmpty(context.Query.Name))
            {
                expr |= SqlExprHelper.GenerateWhere<Account>(x => x.Name.Contains(context.Query.Name));
            }

            return _crudService.GetPageAsync<Account, AccountDto>(
                page,
                limit,
                x => x.Where(expr));
        }
    }
}