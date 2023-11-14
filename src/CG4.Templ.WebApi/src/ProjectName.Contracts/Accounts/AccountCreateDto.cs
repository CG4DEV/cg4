using System.ComponentModel.DataAnnotations.Schema;

namespace ProjectName.Contracts.Accounts
{
    public class AccountCreateDto
    {
        /// <summary>
        /// Account's login.
        /// </summary>
        [Column("login")]
        public string Login { get; set; }

        /// <summary>
        /// Account's last name.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Account's password.
        /// </summary>
        [Column("password")]
        public string Password { get; set; }
    }
}
