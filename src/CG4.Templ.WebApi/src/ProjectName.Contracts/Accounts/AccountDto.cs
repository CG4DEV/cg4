using System.ComponentModel.DataAnnotations.Schema;

namespace ProjectName.Contracts.Accounts
{
    public class AccountDto
    {
        /// <summary>
        /// Account's identifire.
        /// </summary>
        public long Id { get; set; }

        /// <summary>
        /// Account's login.
        /// </summary>
        public string Login { get; set; }

        /// <summary>
        /// Account's last name.
        /// </summary>
        public string Name { get; set; }
    }
}
