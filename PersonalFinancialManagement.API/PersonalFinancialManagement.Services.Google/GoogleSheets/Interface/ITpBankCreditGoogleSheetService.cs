using PersonalFinancialManagement.Models.Dtos.RawTransactions;

namespace PersonalFinancialManagement.GoogleServices.GoogleSheets.Interface;

public interface ITpBankCreditGoogleSheetService
{
    Task ExecuteAsync(List<List<object>> creditWalletGoogles);
    Task<GetOldDataInGoogleSheetResult?> GetOldDataInGoogleSheetAsync();
}