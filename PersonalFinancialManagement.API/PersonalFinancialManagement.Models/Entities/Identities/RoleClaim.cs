using Microsoft.AspNetCore.Identity;

namespace PersonalFinancialManagement.Models.Entities.Identities
{
    public class RoleClaim : IdentityRoleClaim<string>
    {
        public virtual Role? Role { get; set; }
    }
}
