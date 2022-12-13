using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CG4.DataAccess.Domain;
using ProjectName.Domain.Entities;

namespace ProjectName.Integration.Tests.Story
{
    public abstract class BaseTestStory : IAsyncLifetime
    {
        protected readonly DatabaseFixture _fixure;

        protected readonly IList<User> _users;

        protected BaseTestStory()
        {
            _fixure = new DatabaseFixture();

            _users = new List<User>();
        }

        public async Task InitializeAsync()
        {
            await InsertUserAsync();
        }

        public Task DisposeAsync()
        {
            var listToRemove = new List<EntityBase>();

            listToRemove.AddRange(_users);

            return _fixure.CleanUpAsync(listToRemove);
        }

        protected async Task<User> InsertUserAsync(User user)
        {
            await _fixure.CrudService.CreateAsync(user);
            _users.Add(user);
            return user;
        }

        protected Task<User> InsertUserAsync()
        {
            return InsertUserAsync(new User
            {
                Login = "Test login",
                Password = "Test password",
            });
        }
    }
}
