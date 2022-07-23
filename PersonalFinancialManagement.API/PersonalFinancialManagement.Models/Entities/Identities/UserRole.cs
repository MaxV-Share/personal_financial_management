﻿using Microsoft.AspNetCore.Identity;
using PersonalFinancialManagement.Models.Entities.Identities;

namespace PersonalFinancialManagement.Models.Entities.Identities
{
    public class UserRole : IdentityUserRole<string>
    {
        public virtual Role? Role { get; set; }

        public virtual User? User { get; set; }

    }
}