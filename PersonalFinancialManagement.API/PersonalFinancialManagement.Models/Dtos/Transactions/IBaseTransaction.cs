namespace PersonalFinancialManagement.Models.Dtos.Transactions;

public interface IBaseTransaction
{
    public double Amount { get; set; }
    public double? TotalAmount { get; set; }
    public string? Description { get; set; }
    public DateTime TransactionDate { get; set; }
    public Guid? FromPaymentAccountId { get; set; }
    public Guid? CategoryId { get; set; }
}