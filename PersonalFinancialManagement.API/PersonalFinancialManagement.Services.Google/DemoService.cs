using Google.Apis.Sheets.v4;
using Microsoft.Extensions.Options;
using PersonalFinancialManagement.Common.Models.Configurations.Google;
using PersonalFinancialManagement.GoogleServices.Interfaces;

namespace PersonalFinancialManagement.GoogleServices;

public class DemoService : IDemoService
{
    private readonly GoogleCloudSetting _googleCloudSetting;

    public DemoService(IOptions<GoogleCloudSetting> googleCloudSetting)
    {
        _googleCloudSetting = googleCloudSetting.Value;
    }

    public async Task Run()
    {
        var clientId = _googleCloudSetting.GoogleSheetSetting.ClientId;
        var clientSecret = _googleCloudSetting.GoogleSheetSetting.ClientSecret;
        var scopes = new[] { SheetsService.Scope.Spreadsheets };
        var userCredential =
            await GoogleAuthenticationService.LoginAsync(
                clientId, clientSecret, scopes);
        var ggSheets =
            new GoogleSheetService(userCredential);
        await ggSheets.GetSheet("1lKVpkoHSCvm7cIiSgJfSBtIEGpk125WvegEorq8jbqw", "NewSheetTest");
    }
}