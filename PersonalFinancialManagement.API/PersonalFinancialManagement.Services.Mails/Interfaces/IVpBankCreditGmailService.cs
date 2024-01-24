namespace PersonalFinancialManagement.Services.Mails.Interfaces;

public interface IVpBankCreditGmailService
{
    Task<List<List<object>>?> GetCreditWalletGoogles(DateTime? fromDateTime = null,
        List<string>? oldUId = null);
}