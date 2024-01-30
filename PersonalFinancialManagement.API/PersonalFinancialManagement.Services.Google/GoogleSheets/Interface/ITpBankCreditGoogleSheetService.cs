namespace PersonalFinancialManagement.GoogleServices.GoogleSheets.Interface;

public interface ITpBankCreditGoogleSheetService
{
    Task ExecuteAsync(List<List<object>> creditWalletGoogles);
    Task<Tuple<List<string>?, DateTime?>?> GetOldDataInGoogleSheetAsync();
}