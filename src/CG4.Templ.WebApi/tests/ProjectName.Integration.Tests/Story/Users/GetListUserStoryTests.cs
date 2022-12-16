using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ProjectName.Domain.Entities;
using ProjectName.Story.Users;

namespace ProjectName.Integration.Tests.Story.Users
{
    public class GetListUserStoryTests : BaseTestStory
    {
        [Fact]
        public async Task CorrectParameters_ReturnsUserList()
        {
            var story = new GetListUserStory();
            var context = new GetListUserStoryContext();

            IEnumerable<User> result = await story.ExecuteAsync(context);
            
            Assert.NotNull(result);
            Assert.True(result.Count() >= 1);
        }
    }
}
