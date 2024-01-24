using System.Globalization;
using Microsoft.Extensions.Logging;
using PersonalFinancialManagement.GoogleServices.GoogleSheets.Interface;

namespace PersonalFinancialManagement.GoogleServices.GoogleSheets;

public class VpBankCreditGoogleSheetService : IVpBankCreditGoogleSheetService
{
    private const string SheetName = "VP Credit PFM";
    private const string RangeMailId = "B2:B";
    private const string SpreadsheetId = "1lKVpkoHSCvm7cIiSgJfSBtIEGpk125WvegEorq8jbqw";
    private readonly GoogleSheetService _googleSheetService;
    private readonly ILogger<VpBankCreditGoogleSheetService> _logger;

    public VpBankCreditGoogleSheetService(GoogleSheetService googleSheetService,
        ILogger<VpBankCreditGoogleSheetService> logger)
    {
        _googleSheetService = googleSheetService;
        _logger = logger;
    }

    public async Task ExecuteAsync(List<List<object>> creditWalletGoogles)
    {
        _logger.LogInformation("Start VpBankCreditGoogleSheetService ExecuteAsync");
        var spreadsheet = await _googleSheetService.GetSheet(
            SpreadsheetId,
            SheetName);
        var headers = new List<object>
        {
            "No", "MainId", "Transaction Id", "Description", "Value", "Transaction Date",
            "WalletId",
            "Reference code", "Balance"
        };
        foreach (var data in creditWalletGoogles)
            await _googleSheetService.AppendRowBelowLastValue(SpreadsheetId,
                spreadsheet!.Properties.Title,
                data,
                headers);
        _logger.LogInformation("End VpBankCreditGoogleSheetService ExecuteAsync");
    }

    public async Task<Tuple<List<string>?, DateTime?>?> GetOldDataInGoogleSheet()
    {
        _logger.LogInformation("Start GetOldDataInGoogleSheet");
        var spreadsheet = await _googleSheetService.GetRangeValue(
            SpreadsheetId,
            SheetName, RangeMailId);
        //spreadsheet.Data;
        var rangeLastUpdate = await _googleSheetService.GetRangeValue(
            SpreadsheetId,
            SheetName, $"F{(spreadsheet?.Count ?? 0) + 1}");

        if (spreadsheet == null || !spreadsheet.Any()) return default;
        var lastDate = rangeLastUpdate[0][0];
        var uIds = spreadsheet.SelectMany(e => e).Select(e => e.ToString() ?? string.Empty)
            .ToList();
        DateTime? lastUpdate = string.IsNullOrEmpty(lastDate.ToString())
            ? null
            : DateTime.ParseExact(lastDate.ToString() ?? string.Empty, "yyyy-MM-dd HH:mm:ss",
                CultureInfo.InvariantCulture);

        _logger.LogInformation("End GetOldDataInGoogleSheet");
        return new Tuple<List<string>?, DateTime?>(uIds, lastUpdate);
    }
}