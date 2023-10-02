namespace PersonalFinancialManagement.Models.Dtos
{
    public class JwtOptions
    {
        public required string Secret { get; set; }
        public required string ValidAudience { get; set; }
        public int ExpiryMinutes { get; set; }
        public required string ValidIssuer { get; set; }
    }
}
