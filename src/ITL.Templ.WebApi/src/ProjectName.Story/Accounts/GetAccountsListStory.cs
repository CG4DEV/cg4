using System.Collections.Generic;
using System.Threading.Tasks;
using ITL.DataAccess;
using ITL.DataAccess.Poco;
using ITL.DataAccess.Poco.Expressions;
using ITL.Executor.Story;
using ProjectName.Contracts.Accounts;
using ProjectName.Domain.Entities;
using ProjectName.Story.Accounts;
using System.Linq;

public class GetAccountsListStory : IStory<GetAccountsListStoryContext, IEnumerable<AccountDto>>
{
    private readonly ICrudService _crudService;

    public GetAccountsListStory(ICrudService crudService)
    {
        _crudService = crudService;
    }

    // TODO move to common logic with GetAccountPageStory
    public async Task<IEnumerable<AccountDto>> ExecuteAsync(GetAccountsListStoryContext context)
    {
        var expr = ExprBoolean.Empty;

        if (!string.IsNullOrEmpty(context.Query.Name))
        {
            expr |= SqlExprHelper.GenerateWhere<Account>(a => a.Name.Contains(context.Query.Name));
        }

        var result = await _crudService.GetAllAsync<Account>(x => x.Where(expr).Limit(context.Limit.GetValueOrDefault(25)));

        // TODO swap to mapper
        return result.Select(x => new AccountDto
        {
            Id = x.Id,
            Login = x.Login,
            Name = x.Name,
        });
    }
}