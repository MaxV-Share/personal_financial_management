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
        var cancel = new CancellationTokenSource();
        cancel.CancelAfter(TimeSpan.FromMinutes(2));
        return await GoogleWebAuthorizationBroker.AuthorizeAsync(secrets, scopes, "user",
            cancel.Token);
    }
}