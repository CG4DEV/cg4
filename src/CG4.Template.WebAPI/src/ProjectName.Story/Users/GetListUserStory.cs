using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CG4.Executor.Story;
using ProjectName.Domain.Entities;

namespace ProjectName.Story.Users
{
    public class GetListUserStory : IStory<GetListUserStoryContext, IEnumerable<User>>
    {
        private static IEnumerable<User> _users = new List<User>
        {
            new User
            {
                Id = 1,
                Login = "First login",
                Password = "First password",
                CreateDate = DateTimeOffset.UtcNow,
                UpdateDate = DateTimeOffset.UtcNow,
            },
            new User
            {
                Id = 2,
                Login = "Second login",
                Password = "Second password",
                CreateDate = DateTimeOffset.UtcNow,
                UpdateDate = DateTimeOffset.UtcNow,
            }
        };

        public GetListUserStory()
        {
        }

        public Task<IEnumerable<User>> ExecuteAsync(GetListUserStoryContext context)
        {
            return Task.FromResult(_users);
        }
    }
}
