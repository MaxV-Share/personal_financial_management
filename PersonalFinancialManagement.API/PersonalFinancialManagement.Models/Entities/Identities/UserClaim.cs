using Microsoft.AspNetCore.Identity;
using PersonalFinancialManagement.Models.Entities.Identities;

namespace PersonalFinancialManagement.Models.Entities.Identities
{
    public class UserClaim : IdentityUserClaim<string>
    {
        public virtual User User { get; set; }
    }
}
