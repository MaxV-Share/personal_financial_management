using Microsoft.AspNetCore.Identity;

namespace PersonalFinancialManagement.Models.Entities.Identities
{
    public class Role : IdentityRole
    {
        public virtual ICollection<UserRole>? UserRoles { get; }
        public virtual ICollection<RoleClaim>? RoleClaims { get; }
    }
}
