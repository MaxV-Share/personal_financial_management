using PersonalFinancialManagement.Common.Models.DTOs;

namespace PersonalFinancialManagement.Models.Dtos.Transactions.Requests;

public class TransactionUpdateRequest : BaseUpdateRequest<Guid>, IBaseTransaction
{
    public string? Name { get; set; }
    public double? Amount { get; set; }
    public double? TotalAmount { get; set; }
    public string? Description { get; set; }
    public DateTime? TransactionDate { get; set; }
    public Guid? FromPaymentAccountId { get; set; }
    public string? FromPaymentAccountName { get; set; }
    public Guid? CategoryId { get; set; }
    public string? CategoryName { get; set; }
}