using PersonalFinancialManagement.Common.Models;

namespace PersonalFinancialManagement.Models.Entities
{
    /// <summary>
    /// Giao dịch
    /// </summary>
    public class Transaction : BaseEntity<Guid>
    {
        public double? Amount { get; set; }
        public double? TotalAmount { get; set; }
        public string? Description { get; set; }
        public string? ImageUrl { get; set; }
        public DateTime? TransactionDate { get; set; }
        public bool? IsReport { get; set; }
        public double? Fees { get; set; }
        //public ImageInfo? ImageInfo { get; set; }
        public virtual Guid? FeesCategoryId { get; set; }
        public virtual TransactionCategory? FeesCategory { get; set; }
        public virtual Guid? FromPaymentAccountId { get; set; }
        public virtual PaymentAccount? FromPaymentAccount { get; set; }
        public virtual Guid? ToPaymentAccountId { get; set; }
        public virtual PaymentAccount? ToPaymentAccount { get; set; }
        public virtual Guid? CategoryId { get; set; }
        public virtual TransactionCategory? Category { get; set; }
    }
}
