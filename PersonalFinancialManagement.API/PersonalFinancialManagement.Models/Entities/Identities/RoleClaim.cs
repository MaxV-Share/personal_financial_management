using Microsoft.AspNetCore.Identity;
using PersonalFinancialManagement.Models.Entities.Identities;

namespace PersonalFinancialManagement.Models.Entities.Identities
{
    public class RoleClaim : IdentityRoleClaim<string>
    {
        public virtual Role? Role { get; set; }
    }
}
