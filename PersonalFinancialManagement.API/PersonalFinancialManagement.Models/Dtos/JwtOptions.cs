namespace PersonalFinancialManagement.Models.Dtos;

public class JwtOptions
{
    public JwtOptions()
    {
        Secret = "";
        ValidAudience = "";
        ExpiryMinutes = 0;
        ValidIssuer = "";
    }

    public string Secret { get; set; }
    public string ValidAudience { get; set; }
    public int ExpiryMinutes { get; set; }
    public string ValidIssuer { get; set; }
}