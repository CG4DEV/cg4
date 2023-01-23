using System.Collections.Generic;
using System.Threading.Tasks;
using CG4.DataAccess.Domain;
using ProjectName.Domain.Entities;

namespace ProjectName.Integration.Tests.Story
{
    public abstract class BaseTestStory : IAsyncLifetime
    {
        protected readonly DatabaseFixture _fixure;

        protected readonly IList<Account> _accounts;

        protected BaseTestStory()
        {
            _fixure = new DatabaseFixture();

            _accounts = new List<Account>();
        }

        public async Task InitializeAsync()
        {
            await InsertAccountAsync();
        }

        public Task DisposeAsync()
        {
            var listToRemove = new List<EntityBase>();

            listToRemove.AddRange(_accounts);

            return _fixure.CleanUpAsync(listToRemove);
        }

        protected async Task<Account> InsertAccountAsync(Account account)
        {
            await _fixure.CrudService.CreateAsync(account);
            _accounts.Add(account);
            return account;
        }

        protected Task<Account> InsertAccountAsync()
        {
            return InsertAccountAsync(new Account
            {
                Login = "Test login",
                Password = "Test password",
            });
        }
    }
}
