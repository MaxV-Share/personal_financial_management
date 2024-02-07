using AutoMapper;
using Microsoft.Extensions.Logging;
using PersonalFinancialManagement.Common;
using PersonalFinancialManagement.GoogleServices.GoogleSheets.Interface;
using PersonalFinancialManagement.Models.Dtos.Google;
using PersonalFinancialManagement.Services.Interfaces;
using PersonalFinancialManagement.Services.Mails;
using PersonalFinancialManagement.Services.Mails.Interfaces;

namespace PersonalFinancialManagement.Services.Jobs;

public class TpBankCreditJob(
    ITpBankCreditGmailService tpBankCreditGmailService,
    ITpBankCreditGoogleSheetService tpBankCreditGoogleSheetService,
    IRawTransactionService rawTransactionService,
    ILogger<TpBankCreditJob> logger,
    IMapper mapper
)
{
    public async Task Process()
    {
        try
        {
            logger.LogInformation("Start TpBankCreditJob Process");
            // get old data in DB
            var oldMailInDb =
                await rawTransactionService.GetMailIdsAsync(TpBankCreditGmailService.WalletType);

            var lastSync =
                await rawTransactionService.GetLastSyncByWalletAsync(TpBankCreditGmailService
                    .WalletType);
            if (lastSync == new DateTime())
                lastSync = new DateTime(2023, 11, 1);
            // get old data in Google Sheet
            var oldDataDataInGoogleSheet =
                await tpBankCreditGoogleSheetService.GetOldDataInGoogleSheetAsync();
            logger.LogTrace(
                $"{nameof(oldDataDataInGoogleSheet)}: {oldDataDataInGoogleSheet.TryParseToString()}");
            if (oldDataDataInGoogleSheet != null && oldDataDataInGoogleSheet.LastUpdate < lastSync)
            {
                lastSync = oldDataDataInGoogleSheet.LastUpdate;
            }

            var rawTransactions = await tpBankCreditGmailService.GetCreditWalletGoogles(
                lastSync,
                oldMailInDb
            );
            logger.LogTrace($"{nameof(rawTransactions)}: {rawTransactions.TryParseToString()}");

            var rawCreateRequestTransactions = mapper.Map<List<RawTransactionCreateRequest>>(
                rawTransactions
            );
            logger.LogTrace(
                $"{nameof(rawCreateRequestTransactions)}: {rawCreateRequestTransactions.TryParseToString()}"
            );

            var resultCreate = await rawTransactionService.CreateAsync(
                rawCreateRequestTransactions
            );
            logger.LogTrace($"{nameof(resultCreate)}: {resultCreate.TryParseToString()}");

            var rawGoogleSheetTransactions = rawTransactions
                .OrderBy(e => e.TransactionDate)
                .Select(e => e.ToGoogleSheetList())
                .ToList();
            logger.LogTrace(
                $"{nameof(rawGoogleSheetTransactions)}: {rawGoogleSheetTransactions.TryParseToString()}");

            if (rawGoogleSheetTransactions?.Any() ?? false)
                await tpBankCreditGoogleSheetService.ExecuteAsync(rawGoogleSheetTransactions);

            logger.LogInformation(
                $"End TpBankCreditJob Process with: {rawGoogleSheetTransactions!.Count}");
        }
        catch (Exception e)
        {
            logger.LogError(e, $"End Exception TpBankCreditJob Process: {e.Message}");
        }
    }
}