using Google.Apis.Auth.OAuth2;

namespace PersonalFinancialManagement.GoogleServices;

public class GoogleAuthenticationService
{
    public static async Task<UserCredential> LoginAsync(string googleClientId,
        string googleClientSecret,
        string[] scopes)
    {
        var secrets = new ClientSecrets
        {
            ClientId = googleClientId,
            ClientSecret = googleClientSecret
        };
        return await GoogleWebAuthorizationBroker.AuthorizeAsync(secrets, scopes, "user",
            CancellationToken.None);
    }
}