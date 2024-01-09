using Google.Apis.Auth.OAuth2;
using Google.Apis.Services;
using Google.Apis.Sheets.v4;

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

    public static async Task<SheetsService> LoginAsync(string apiKey)
    {
        try
        {
            if (string.IsNullOrEmpty(apiKey))
                throw new ArgumentNullException("api Key");

            return new SheetsService(new BaseClientService.Initializer
            {
                ApiKey = apiKey,
                ApplicationName =
                    "PFM"
            });
        }
        catch (Exception ex)
        {
            throw new Exception("Failed to create new Sheets Service", ex);
        }
    }
}