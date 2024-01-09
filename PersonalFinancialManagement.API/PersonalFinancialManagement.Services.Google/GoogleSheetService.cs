using Google.Apis.Auth.OAuth2;
using Google.Apis.Services;
using Google.Apis.Sheets.v4;
using Google.Apis.Sheets.v4.Data;

namespace PersonalFinancialManagement.GoogleServices;

public class GoogleSheetService
{
    private readonly UserCredential _userCredential;

    public GoogleSheetService(UserCredential userCredential)
    {
        _userCredential = userCredential;
    }

    public async Task<Spreadsheet> GetSpreadsheetById(string id)
    {
        using var sheetsService = new SheetsService(new BaseClientService.Initializer
            { HttpClientInitializer = _userCredential, ApplicationName = "PFM" });
        return await sheetsService.Spreadsheets.Get(id)
            .ExecuteAsync();
    }

    public async Task<Spreadsheet> GetSheet(string documentName, string sheetName)
    {
        if (string.IsNullOrEmpty(documentName))
            throw new ArgumentNullException(nameof(documentName));

        var spreadsheet = await GetSpreadsheetById(documentName);
        var sheet = spreadsheet.GetSheetByName(sheetName);
        return spreadsheet;
    }
}