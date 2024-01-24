namespace PersonalFinancialManagement.GoogleServices.Models;

public class CreditWalletGoogleModel
{
    public string No { get; set; } = "";
    public string MailId { get; set; } = "";
    public string TransactionId { get; set; } = "";
    public string Description { get; set; } = "";
    public double Value { get; set; }
    public DateTime TransactionDate { get; set; }
    public string WalletId { get; set; } = "";
    public string ReferenceCode { get; set; } = "";
    public double Balance { get; set; }

    public List<object> ToGoogleSheetList()
    {
        return new List<object>
        {
            No,
            MailId,
            TransactionId,
            Description,
            Value,
            TransactionDate.ToString("yyyy-MM-dd HH:mm:ss"),
            WalletId,
            ReferenceCode,
            Balance
        };
    }
}