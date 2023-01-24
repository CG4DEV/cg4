using System.Text;
using System.Threading.Tasks;
using CG4.DataAccess.Domain;
using CG4.Executor.Story;
using CG4.Impl.Dapper.Crud;
using ProjectName.Common;
using ProjectName.Domain.Entities;

namespace ProjectName.Story.Accounts
{
    public class GetAccountsPageStory : IStory<GetAccountsPageStoryContext, PageResult<Account>>
    {
        private readonly ICrudService _crudService;
        private readonly ISearchService _searchService;

        private const string ACCOUNT_QUERY = @"SELECT 
                                                a.id,
                                                a.login,
                                                a.password,
                                                a.create_date AS CreateDate,
                                                a.update_date AS UpdateDate
                                            FROM 
                                                accounts AS a 
                                            WHERE 
                                                a.id = ANY(@Ids)";
        
        private const string ACCOUNT_COUNT_QUERY = @"SELECT 
                                                      Count(a.*) 
                                                  FROM 
                                                      accounts AS a 
                                                  WHERE 
                                                      a.id = ANY(@Ids)";

        private const string ACCOUNT_SEARCH_QUERY = @"SELECT 
                                                       a.id 
                                                   FROM 
                                                       accounts AS a";

        public GetAccountsPageStory(ICrudService crudService, ISearchService searchService)
        {
            _crudService = crudService;
            _searchService = searchService;
        }
        
        public async Task<PageResult<Account>> ExecuteAsync(GetAccountsPageStoryContext context)
        {
            var searchQuery = new StringBuilder(ACCOUNT_SEARCH_QUERY);
            var limit = context.Limit ?? 25;
            var page = context.Page ?? 0;

            if (!string.IsNullOrEmpty(context.FastSearch))
            {
                if (long.TryParse(context.FastSearch, out var searchToLong))
                {
                    searchQuery.Append($" WHERE a.id = {searchToLong}");
                }
                else
                {
                    searchQuery.Append($" WHERE a.login LIKE '{context.FastSearch}'");
                }
            }

            searchQuery.Append(" OFFSET @Offset");
            searchQuery.Append(" LIMIT @Limit");

            var accountsIds = await _searchService.SearchAsync(searchQuery.ToString(), new
            {
                Limit = limit,
                Offset = page * limit
            });

            var accountsTask = _crudService.QueryAsync<Account>(ACCOUNT_QUERY, new { Ids = accountsIds });
            var accountsCountTask = _crudService.QuerySingleOrDefaultAsync<int>(ACCOUNT_COUNT_QUERY, new { Ids = accountsIds });

            await Task.WhenAll(accountsTask, accountsCountTask);

            return new PageResult<Account>
            {
                Data = accountsTask.Result,
                Page = page,
                Count = accountsCountTask.Result,
                FilteredCount = accountsCountTask.Result
            };
        }
    }
}