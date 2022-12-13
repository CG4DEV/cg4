using System.ComponentModel.DataAnnotations.Schema;
using CG4.DataAccess.Domain;

namespace ProjectName.Domain.Entities
{
    [Table("users")]
    public class User : EntityBase
    {
        /// <summary>
        /// User's login.
        /// </summary>
        [Column("login")]
        public string Login { get; set; }

        /// <summary>
        /// User's password.
        /// </summary>
        [Column("password")]
        public string Password { get; set; }
    }
}
