namespace PersonalFinancialManagement.GoogleServices.GoogleSheets.Interface;

public interface IVpBankCreditGoogleSheetService
{
    Task ExecuteAsync(List<List<object>> creditWalletGoogles);
    Task<Tuple<List<string>?, DateTime?>?> GetOldDataInGoogleSheet();
}