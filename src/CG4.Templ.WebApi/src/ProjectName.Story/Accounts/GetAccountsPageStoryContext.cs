using CG4.DataAccess.Domain;
using CG4.Executor;
using ProjectName.Domain.Entities;

namespace ProjectName.Story.Accounts
{
    public class GetAccountsPageStoryContext : IResult<PageResult<Account>>
    {
        /// <summary>
        /// Количество выводимых строк. По умолчанию - 25.
        /// </summary>
        public int? Limit { get; set; }
        
        /// <summary>
        /// Текущая страница.
        /// </summary>
        public int? Page { get; set; }
        
        /// <summary>
        /// Строка полиска.
        /// </summary>
        public string FastSearch { get; set; }
    }
}