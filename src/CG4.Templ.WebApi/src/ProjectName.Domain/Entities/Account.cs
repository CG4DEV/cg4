using System.ComponentModel.DataAnnotations.Schema;
using CG4.DataAccess.Domain;

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
    }
}
