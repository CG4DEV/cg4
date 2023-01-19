using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CG4.DataAccess.Domain;
using CG4.Executor.Story;
using CG4.Impl.Dapper.Crud;
using ProjectName.Common;
using ProjectName.Domain.Entities;

namespace ProjectName.Story.Users
{
    public class GetUsersPageStory : IStory<GetUsersPageStoryContext, PageResult<User>>
    {
        private readonly ICrudService _crudService;
        private readonly ISearchService _searchService;

        private const string USER_QUERY = @"SELECT 
                                                u.id,
                                                u.login,
                                                u.password,
                                                u.create_date AS CreateDate,
                                                u.update_date AS UpdateDate
                                            FROM 
                                                users AS u 
                                            WHERE 
                                                u.id = ANY(@Ids)";
        
        private const string USER_COUNT_QUERY = @"SELECT 
                                                      Count(u.*) 
                                                  FROM 
                                                      users AS u 
                                                  WHERE 
                                                      u.id = ANY(@Ids)";

        private const string USER_SEARCH_QUERY = @"SELECT 
                                                       u.id 
                                                   FROM 
                                                       users AS u";

        public GetUsersPageStory(ICrudService crudService, ISearchService searchService)
        {
            _crudService = crudService;
            _searchService = searchService;
        }
        
        public async Task<PageResult<User>> ExecuteAsync(GetUsersPageStoryContext context)
        {
            var searchQuery = new StringBuilder(USER_SEARCH_QUERY);
            var limit = context.Limit ?? 25;
            var page = context.Page ?? 0;

            if (!string.IsNullOrEmpty(context.FastSearch))
            {
                if (long.TryParse(context.FastSearch, out var searchToLong))
                {
                    searchQuery.Append(" WHERE u.id = @FastSearch");
                }
                else
                {
                    searchQuery.Append(" WHERE u.login LIKE @FastSearch");
                }
            }

            searchQuery.Append(" OFFSET @Offset");
            searchQuery.Append(" LIMIT @Limit");

            var usersIds = await _searchService.SearchAsync(searchQuery.ToString(), new
            {
                FastSearch = context.FastSearch,
                Limit = limit,
                Offset = page * limit
            });

            var users = _crudService.QueryAsync<User>(USER_QUERY, new { Ids = usersIds });
            var usersCount = _crudService.QuerySingleOrDefaultAsync<int>(USER_COUNT_QUERY, new { Ids = usersIds });

            await Task.WhenAll(users, usersCount);

            return new PageResult<User>
            {
                Data = users.Result,
                Page = page,
                Count = usersCount.Result,
                FilteredCount = usersCount.Result
            };
        }
    }
}