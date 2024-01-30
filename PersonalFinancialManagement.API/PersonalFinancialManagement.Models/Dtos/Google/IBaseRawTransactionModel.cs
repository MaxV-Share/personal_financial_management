namespace PersonalFinancialManagement.Models.Dtos.Google;

internal interface IBaseRawTransactionModel
{
    public string No { get; set; }
    public string MailId { get; set; }
    public string TransactionId { get; set; }
    public string Description { get; set; }
    public double Amount { get; set; }
    public DateTime TransactionDate { get; set; }
    public string WalletId { get; set; }
    public string WalletType { get; set; }
    public string ReferenceCode { get; set; }
    public double Balance { get; set; }
}