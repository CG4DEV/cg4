using CG4.DataAccess.Domain;
using CG4.Executor;
using ProjectName.Domain.Entities;

namespace ProjectName.Story.Users
{
    public class GetUsersPageStoryContext : IResult<PageResult<User>>
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