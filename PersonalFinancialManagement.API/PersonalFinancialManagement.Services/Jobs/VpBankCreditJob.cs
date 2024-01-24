using Microsoft.Extensions.Logging;
using PersonalFinancialManagement.GoogleServices.GoogleSheets.Interface;
using PersonalFinancialManagement.Services.Mails.Interfaces;

namespace PersonalFinancialManagement.Services.Jobs;

public class VpBankCreditJob
{
    private readonly ILogger<VpBankCreditJob> _logger;
    private readonly IVpBankCreditGmailService _vpBankCreditGmailService;
    private readonly IVpBankCreditGoogleSheetService _vpBankCreditGoogleSheetService;

    public VpBankCreditJob(IVpBankCreditGmailService vpBankCreditGmailService,
        IVpBankCreditGoogleSheetService vpBankCreditGoogleSheetService,
        ILogger<VpBankCreditJob> logger)
    {
        _vpBankCreditGmailService = vpBankCreditGmailService;
        _vpBankCreditGoogleSheetService = vpBankCreditGoogleSheetService;
        _logger = logger;
    }

    public async Task Process()
    {
        try
        {
            _logger.LogInformation("Start VpBankCreditJob Process");
            var oldData = await _vpBankCreditGoogleSheetService.GetOldDataInGoogleSheet();
            var creditWalletGoogles =
                await _vpBankCreditGmailService.GetCreditWalletGoogles(oldData?.Item2,
                    oldData?.Item1);
            if (creditWalletGoogles != null)
                await _vpBankCreditGoogleSheetService.ExecuteAsync(creditWalletGoogles);
            _logger.LogInformation(
                $"End VpBankCreditJob Process with: {creditWalletGoogles.Count}");
        }
        catch (Exception e)
        {
            _logger.LogError(e, $"End Exception VpBankCreditJob Process: {e.Message}");
        }
    }
}