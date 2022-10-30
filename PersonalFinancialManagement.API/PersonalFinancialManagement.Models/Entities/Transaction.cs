using PersonalFinancialManagement.Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PersonalFinancialManagement.Models.Entities
{
    /// <summary>
    /// Giao dịch
    /// </summary>
    public class Transaction : BaseEntity<Guid>
    {
        public double? Amount { get; set; }
        public string? Description { get; set; }
        public string? ImageUrl { get; set; }
        public DateTime? TransactionDate { get; set; }
        public bool? IsReport { get; set; }
        public bool? Fees { get; set; }
        public ImageInfo? ImageInfo { get; set; }
        public TransactionCategory? FeesCategory { get; set; }
        public PaymentAccount? FromPaymentAccount { get; set; }
        public PaymentAccount? ToPaymentAccount { get; set; }
        public TransactionCategory? Category { get; set; }
    }
}
