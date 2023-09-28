using PersonalFinancialManagement.Common.Models.DTOs;

namespace PersonalFinancialManagement.Models.Dtos.Transactions;

public class TransactionViewModel : BaseViewModel<Guid>, IBaseTransaction
{
    public double Amount { get; set; }
    public double? TotalAmount { get; set; }
    public string? Description { get; set; }
    public DateTime TransactionDate { get; set; }
    public Guid? FromPaymentAccountId { get; set; }
    public string? FromPaymentAccountName { get; set; }
    public Guid? CategoryId { get; set; }
    public string? CategoryName { get; set; }
}