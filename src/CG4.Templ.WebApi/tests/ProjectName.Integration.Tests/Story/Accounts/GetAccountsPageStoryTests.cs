using System.Threading.Tasks;
using ProjectName.Domain.Entities;
using ProjectName.Story.Accounts;

namespace ProjectName.Integration.Tests.Story.Accounts
{
    public class GetAccountsPageStoryTests : BaseTestStory
    {
        [Fact]
        public async Task CorrectParameters_ReturnCorrectPage()
        {
            var firstAccount = await InsertAccountAsync();
            var secondAccount = await InsertAccountAsync();
            
            var context = new GetAccountsPageStoryContext
            {
                Page = 0,
                Limit = 25
            };

            var story = new GetAccountsPageStory(_fixure.CrudService, _fixure.SearchService);

            var result = await story.ExecuteAsync(context);
            
            Assert.Equal(0, result.Page);
            Assert.True(result.Count >= 2);
            Assert.True(result.FilteredCount >= 2);
            Assert.NotNull(result.Data);
            Assert.Contains(result.Data, x => 
                x.Password == firstAccount.Password 
                && x.Login == firstAccount.Login);
            
            Assert.Contains(result.Data, x => 
                x.Password == secondAccount.Password 
                && x.Login == secondAccount.Login);
        }

        [Fact]
        public async Task FastSearchSet_ReturnCorrectPage()
        {
            var firstAccount = await InsertAccountAsync(new Account
            {
                Login = "Test FastSearch Login",
                Password = "Test password"
            });
            
            var secondAccount = await InsertAccountAsync();
            
            var context = new GetAccountsPageStoryContext { FastSearch = firstAccount.Login};
            var story = new GetAccountsPageStory(_fixure.CrudService, _fixure.SearchService);

            var result = await story.ExecuteAsync(context);
            
            Assert.Equal(0, result.Page);
            Assert.Equal(1, result.Count);
            Assert.Equal(1, result.FilteredCount);
            Assert.NotNull(result.Data);
            Assert.Contains(result.Data, x => 
                x.Password == firstAccount.Password 
                && x.Login == firstAccount.Login);
        }
    }
}