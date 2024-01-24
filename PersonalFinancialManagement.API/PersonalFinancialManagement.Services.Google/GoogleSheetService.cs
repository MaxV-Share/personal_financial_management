using Google.Apis.Auth.OAuth2;
using Google.Apis.Services;
using Google.Apis.Sheets.v4;
using Google.Apis.Sheets.v4.Data;
using Microsoft.Extensions.Options;
using PersonalFinancialManagement.Common.Models.Configurations.Google;

namespace PersonalFinancialManagement.GoogleServices;

public class GoogleSheetService
{
    private readonly GoogleCloudSetting _googleCloudSetting;
    private UserCredential? _userCredential;

    public GoogleSheetService(IOptions<GoogleCloudSetting> googleCloudSetting)
    {
        _googleCloudSetting = googleCloudSetting.Value;
    }

    public async Task<Spreadsheet> GetSpreadsheetById(string id)
    {
        var clientId = _googleCloudSetting.GoogleSheetSetting.ClientId;
        var clientSecret = _googleCloudSetting.GoogleSheetSetting.ClientSecret;
        var scopes = new[] { SheetsService.Scope.Spreadsheets };
        _userCredential =
            await GoogleAuthenticationService.LoginAsync(
                clientId, clientSecret, scopes);
        using var sheetsService = new SheetsService(new BaseClientService.Initializer
        {
            HttpClientInitializer = _userCredential,
            ApplicationName = _googleCloudSetting.ApplicationName
        });
        return await sheetsService.Spreadsheets.Get(id)
            .ExecuteAsync();
    }

    public async Task<Sheet?> GetSheet(string documentName, string sheetName)
    {
        if (string.IsNullOrEmpty(documentName))
            throw new ArgumentNullException(nameof(documentName));

        var spreadsheet = await GetSpreadsheetById(documentName);
        var result = spreadsheet.GetSheetByName(sheetName);
        return result;
    }

    public async Task<IList<IList<object>>> GetRangeValue(string spreadsheetId,
        string sheetName, string range)
    {
        var clientId = _googleCloudSetting.GoogleSheetSetting.ClientId;
        var clientSecret = _googleCloudSetting.GoogleSheetSetting.ClientSecret;
        var scopes = new[] { SheetsService.Scope.Spreadsheets };
        _userCredential =
            await GoogleAuthenticationService.LoginAsync(
                clientId, clientSecret, scopes);
        using var sheetsService = new SheetsService(new BaseClientService.Initializer
        {
            HttpClientInitializer = _userCredential,
            ApplicationName = _googleCloudSetting.ApplicationName
        });
        // Get the current data in the last column
        var request = sheetsService.Spreadsheets.Values.Get(spreadsheetId, $"{sheetName}!{range}");
        var response = await request.ExecuteAsync();
        return response.Values;
    }

    public async Task AppendRowBelowLastValue(string spreadsheetId,
        string sheetName, List<object> values, List<object> headers)
    {
        var lastColumn = await GetLastRowIndex(spreadsheetId, sheetName);
        lastColumn++;
        if (lastColumn == 1) await AppendRow(spreadsheetId, sheetName, headers, lastColumn);

        // Add a row to the spreadsheet below the last value
        await AppendRow(spreadsheetId, sheetName, values, lastColumn);
    }

    public async Task<int> GetLastRowIndex(string spreadsheetId, string sheetName)
    {
        var clientId = _googleCloudSetting.GoogleSheetSetting.ClientId;
        var clientSecret = _googleCloudSetting.GoogleSheetSetting.ClientSecret;
        var scopes = new[] { SheetsService.Scope.Spreadsheets };
        _userCredential =
            await GoogleAuthenticationService.LoginAsync(
                clientId, clientSecret, scopes);
        using var sheetsService = new SheetsService(new BaseClientService.Initializer
        {
            HttpClientInitializer = _userCredential,
            ApplicationName = _googleCloudSetting.ApplicationName
        });
        // Get the current data in the last column
        var getLastColumnRequest =
            sheetsService.Spreadsheets.Values.Get(spreadsheetId, sheetName + "!B:B");
        var lastColumnResponse = await getLastColumnRequest.ExecuteAsync();
        var lastColumn = lastColumnResponse.Values?.Count ?? 0;
        return lastColumn;
    }

    public async Task AppendRow(string spreadsheetId,
        string sheetName, List<object> values, int rowIndex)
    {
        var clientId = _googleCloudSetting.GoogleSheetSetting.ClientId;
        var clientSecret = _googleCloudSetting.GoogleSheetSetting.ClientSecret;
        var scopes = new[] { SheetsService.Scope.Spreadsheets };
        _userCredential =
            await GoogleAuthenticationService.LoginAsync(
                clientId, clientSecret, scopes);
        using var sheetsService = new SheetsService(new BaseClientService.Initializer
        {
            HttpClientInitializer = _userCredential,
            ApplicationName = _googleCloudSetting.ApplicationName
        });
        // Create a new row
        var appendRequest = sheetsService.Spreadsheets.Values.Append(new ValueRange
        {
            Values = new List<IList<object>> { values }
        }, spreadsheetId, sheetName + "!A" + rowIndex);
        // Choose the input value type (RAW, USER_ENTERED, ...)
        appendRequest.ValueInputOption = SpreadsheetsResource.ValuesResource.AppendRequest
            .ValueInputOptionEnum.USERENTERED;
        appendRequest.InsertDataOption = SpreadsheetsResource.ValuesResource.AppendRequest
            .InsertDataOptionEnum.INSERTROWS;

        // Execute the request
        var appendResponse = await appendRequest.ExecuteAsync();
    }
}