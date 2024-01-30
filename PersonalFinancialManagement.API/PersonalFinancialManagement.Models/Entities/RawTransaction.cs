using PersonalFinancialManagement.Common.Models;

namespace PersonalFinancialManagement.Models.Entities;

public class RawTransaction : BaseEntity<Guid>
{
    public string? No { get; set; }
    public string? MailId { get; set; }
    public string? TransactionId { get; set; }
    public string? Description { get; set; }
    public double Amount { get; set; }
    public DateTime TransactionDate { get; set; }
    public string? WalletId { get; set; }
    public string? WalletType { get; set; }
    public string? ReferenceCode { get; set; }
    public double Balance { get; set; }
    public double RawString { get; set; }
}