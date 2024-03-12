﻿using System.ComponentModel.DataAnnotations.Schema;

namespace ProjectName.Contracts.Accounts
{
    public class AccountCreateDto
    {
        /// <summary>
        /// Account's login.
        /// </summary>
        public string Login { get; set; }

        /// <summary>
        /// Account's last name.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Account's password.
        /// </summary>
        public string Password { get; set; }
    }
}
