using PersonalFinancialManagement.Models.Dtos.Google;

namespace PersonalFinancialManagement.Services.Mails.Interfaces;

public interface ITpBankCreditGmailService
{
    Task<List<RawTransactionViewModel>> GetCreditWalletGoogles(DateTime? fromDateTime = null,
        List<string>? oldUId = null);
}