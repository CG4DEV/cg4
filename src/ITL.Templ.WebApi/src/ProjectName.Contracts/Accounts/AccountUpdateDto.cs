using System.ComponentModel.DataAnnotations.Schema;

namespace ProjectName.Contracts.Accounts
{
    public class AccountUpdateDto : AccountCreateDto
    {
        /// <summary>
        /// Account's old password.
        /// </summary>
        public string OldPassword { get; set; }
    }
}
