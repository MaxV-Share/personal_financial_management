using AutoMapper;
using Microsoft.Extensions.Logging;
using PersonalFinancialManagement.Common;
using PersonalFinancialManagement.GoogleServices.GoogleSheets.Interface;
using PersonalFinancialManagement.Models.Dtos.Google;
using PersonalFinancialManagement.Services.Interfaces;
using PersonalFinancialManagement.Services.Mails;
using PersonalFinancialManagement.Services.Mails.Interfaces;

namespace PersonalFinancialManagement.Services.Jobs;

public class VpBankCreditJob(
    IVpBankCreditGmailService vpBankCreditGmailService,
    IVpBankCreditGoogleSheetService vpBankCreditGoogleSheetService,
    IRawTransactionService rawTransactionService,
    ILogger<VpBankCreditJob> logger,
    IMapper mapper
)
{
    public async Task Process()
    {
        try
        {
            logger.LogInformation("Start VpBankCreditJob Process");
            // get old data in DB
            var oldMailInDb =
                await rawTransactionService.GetMailIdsAsync(VpBankCreditGmailService.WalletType);

            var lastSync =
                await rawTransactionService.GetLastSyncByWalletAsync(VpBankCreditGmailService
                    .WalletType);
            if (lastSync == new DateTime())
                lastSync = new DateTime(2023, 11, 1);
            // get old data in Google Sheet
            //var oldData = await vpBankCreditGoogleSheetService.GetOldDataInGoogleSheetAsync();
            //logger.LogTrace($"{nameof(oldData)}: {oldData.TryParseToString()}");
            //if (oldData >)
            //{

            //}

            var rawTransactions = await vpBankCreditGmailService.GetCreditWalletGoogles(
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

            //var rawGoogleSheetTransactions = rawTransactions.OrderBy(e => e.TransactionDate)
            //    .Select(e => e.ToGoogleSheetList()).ToList();
            //logger.LogTrace(
            //    $"{nameof(rawGoogleSheetTransactions)}: {rawGoogleSheetTransactions.TryParseToString()}");

            //if (rawGoogleSheetTransactions?.Any() ?? false)
            //    await vpBankCreditGoogleSheetService.ExecuteAsync(rawGoogleSheetTransactions);

            //logger.LogInformation(
            //    $"End VpBankCreditJob Process with: {rawGoogleSheetTransactions!.Count}");
        }
        catch (Exception e)
        {
            logger.LogError(e, $"End Exception VpBankCreditJob Process: {e.Message}");
        }
    }
}