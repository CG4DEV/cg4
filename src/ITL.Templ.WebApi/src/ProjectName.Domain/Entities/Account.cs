using System.ComponentModel.DataAnnotations.Schema;
using ITL.DataAccess.Domain;

namespace ProjectName.Domain.Entities
{
    [Table("accounts")]
    public class Account : EntityBase
    {
        /// <summary>
        /// Account's login.
        /// </summary>
        [Column("login")]
        public string Login { get; set; }

        /// <summary>
        /// Account's password.
        /// </summary>
        [Column("password")]
        public string Password { get; set; }

        /// <summary>
        /// Account's last name.
        /// </summary>
        [Column("name")]
        public string Name { get; set; }
    }
}