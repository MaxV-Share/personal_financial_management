using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
using PersonalFinancialManagement.Models.Entities.Identities;

namespace PersonalFinancialManagement.Models.Entities.Identities
{
    public class Role : IdentityRole
    {
        public virtual ICollection<UserRole> UserRoles { get; }
        public virtual ICollection<RoleClaim> RoleClaims { get; }
    }
}
