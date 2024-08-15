using System.Globalization;
using Microsoft.Extensions.Logging;
using PersonalFinancialManagement.Common;
using PersonalFinancialManagement.GoogleServices.GoogleSheets.Interface;

namespace PersonalFinancialManagement.GoogleServices.GoogleSheets;

public class VpBankCreditGoogleSheetService(
    GoogleSheetService googleSheetService,
    ILogger<VpBankCreditGoogleSheetService> logger
)
    : BasePFMSheetService(logger),
        IVpBankCreditGoogleSheetService
{
    private const string SheetName = "VP Credit PFM";

    public async Task ExecuteAsync(List<List<object>> creditWalletGoogles)
    {
        _logger.LogInformation("Start VpBankCreditGoogleSheetService ExecuteAsync");
        var spreadsheet = await googleSheetService.GetSheet(SpreadsheetId, SheetName);
        var headers = new List<object>
        {
            "No",
            "MainId",
            "Transaction Id",
            "Description",
            "Value",
            "Transaction Date",
            "WalletId",
            "Reference code",
            "Balance"
        };
        foreach (var data in creditWalletGoogles)
            await googleSheetService.AppendRowBelowLastValue(
                SpreadsheetId,
                spreadsheet!.Properties.Title,
                data,
                headers
            );
        _logger.LogInformation("End VpBankCreditGoogleSheetService ExecuteAsync");
    }

    public async Task<Tuple<List<string>?, DateTime?>?> GetOldDataInGoogleSheetAsync()
    {
        _logger.LogInformation("Start GetOldDataInGoogleSheet");
        var spreadsheet = await googleSheetService.GetRangeValue(
            SpreadsheetId,
            SheetName,
            RangeMailId
        );

        _logger.LogTrace($"{nameof(spreadsheet)}: {spreadsheet.TryParseToString()}");

        if (spreadsheet == null || !spreadsheet.Any())
            return default;

        var rangeLastUpdate = await googleSheetService.GetRangeValue(
            SpreadsheetId,
            SheetName,
            $"F{spreadsheet.Count + 1}"
        );

        _logger.LogTrace($"{nameof(rangeLastUpdate)}: {rangeLastUpdate.TryParseToString()}");

        if (rangeLastUpdate == null || !rangeLastUpdate.Any())
            return default;

        var lastDate = rangeLastUpdate[0][0];
        var uIds = spreadsheet
            .SelectMany(e => e)
            .Select(e => e.ToString() ?? string.Empty)
            .ToList();

        DateTime? lastUpdate = string.IsNullOrEmpty(lastDate.ToString())
            ? null
            : DateTime.ParseExact(
                lastDate.ToString() ?? string.Empty,
                "yyyy-MM-dd HH:mm:ss",
                CultureInfo.InvariantCulture
            );

        _logger.LogTrace($"{nameof(uIds)}: {uIds.TryParseToString()}");
        _logger.LogTrace($"{nameof(lastUpdate)}: {lastUpdate}");
        _logger.LogInformation("End GetOldDataInGoogleSheet");

        return new Tuple<List<string>?, DateTime?>(uIds, lastUpdate);
    }
}