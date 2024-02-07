using PersonalFinancialManagement.Models.Dtos.RawTransactions;

namespace PersonalFinancialManagement.GoogleServices.GoogleSheets.Interface;

public interface IVpBankCreditGoogleSheetService
{
    Task ExecuteAsync(List<List<object>> creditWalletGoogles);
    Task<GetOldDataInGoogleSheetResult?> GetOldDataInGoogleSheetAsync();
}