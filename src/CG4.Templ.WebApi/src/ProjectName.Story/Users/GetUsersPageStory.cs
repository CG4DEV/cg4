using System.Threading.Tasks;
using CG4.DataAccess.Domain;
using CG4.Executor.Story;
using CG4.Impl.Dapper.Crud;
using CG4.Impl.Dapper.Poco;
using CG4.Impl.Dapper.Poco.Expressions;
using ProjectName.Domain.Entities;

namespace ProjectName.Story.Users
{
    public class GetUsersPageStory : IStory<GetUsersPageStoryContext, PageResult<User>>
    {
        private readonly ICrudService _crudService;

        public GetUsersPageStory(ICrudService crudService)
        {
            _crudService = crudService;
        }
        
        public Task<PageResult<User>> ExecuteAsync(GetUsersPageStoryContext context)
        {
            var expr = ExprBoolean.Empty;

            if (!string.IsNullOrEmpty(context.FastSearch))
            {
                if (long.TryParse(context.FastSearch, out var searchToLong))
                {
                    expr |= SqlExprHelper.GenerateWhere<User>(x => x.Id == searchToLong);
                }
                else
                {
                    expr |= SqlExprHelper.GenerateWhere<User>(x => x.Login.Contains(context.FastSearch));
                }
            }

            return _crudService.GetPageAsync<User>(
                context.Page.GetValueOrDefault(),
                context.Limit.GetValueOrDefault(),
                x => x.Where(expr));
        }
    }
}