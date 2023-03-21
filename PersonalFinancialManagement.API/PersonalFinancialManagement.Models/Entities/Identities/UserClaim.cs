using Microsoft.AspNetCore.Identity;

namespace PersonalFinancialManagement.Models.Entities.Identities
{
    public class UserClaim : IdentityUserClaim<string>
    {
        public virtual User User { get; set; }
    }
}
