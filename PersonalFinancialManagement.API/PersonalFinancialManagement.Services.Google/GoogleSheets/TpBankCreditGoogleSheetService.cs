using System.Globalization;
using Microsoft.Extensions.Logging;
using PersonalFinancialManagement.Common;
using PersonalFinancialManagement.GoogleServices.GoogleSheets.Interface;
using PersonalFinancialManagement.Models.Dtos.RawTransactions;

namespace PersonalFinancialManagement.GoogleServices.GoogleSheets;

public class TpBankCreditGoogleSheetService(
    GoogleSheetService googleSheetService,
    ILogger<TpBankCreditGoogleSheetService> logger
)
    : BasePFMSheetService(logger),
        ITpBankCreditGoogleSheetService
{
    private const string SheetName = "TP Credit PFM";

    public async Task ExecuteAsync(List<List<object>> creditWalletGoogles)
    {
        logger.LogInformation("Start TpBankCreditGoogleSheetService ExecuteAsync");
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
        logger.LogInformation("End TpBankCreditGoogleSheetService ExecuteAsync");
    }

    public async Task<GetOldDataInGoogleSheetResult?> GetOldDataInGoogleSheetAsync()
    {
        logger.LogInformation("Start GetOldDataInGoogleSheetAsync");
        var spreadsheet = await googleSheetService.GetRangeValue(
            SpreadsheetId,
            SheetName,
            RangeMailId
        );

        logger.LogTrace($"{nameof(spreadsheet)}: {spreadsheet.TryParseToString()}");

        if (spreadsheet == null || !spreadsheet.Any())
            return default;

        var rangeLastUpdate = await googleSheetService.GetRangeValue(
            SpreadsheetId,
            SheetName,
            $"F{spreadsheet.Count + 1}"
        );

        logger.LogTrace($"{nameof(rangeLastUpdate)}: {rangeLastUpdate.TryParseToString()}");

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

        logger.LogTrace($"{nameof(uIds)}: {uIds.TryParseToString()}");
        logger.LogTrace($"{nameof(lastUpdate)}: {lastUpdate}");
        logger.LogInformation("End GetOldDataInGoogleSheetAsync");

        return new GetOldDataInGoogleSheetResult(lastUpdate, uIds);
    }
}