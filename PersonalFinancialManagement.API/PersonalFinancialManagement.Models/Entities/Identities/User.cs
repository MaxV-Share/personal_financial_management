using Microsoft.AspNetCore.Identity;

namespace PersonalFinancialManagement.Models.Entities.Identities
{
    public class User : IdentityUser
    {
        public string? FullName { get; set; }
        public virtual ICollection<UserRole>? UserRoles { get; }
        public virtual ICollection<UserClaim>? UserClaims { get; }
    }
}
