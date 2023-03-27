using PersonalFinancialManagement.Common.Models;

namespace PersonalFinancialManagement.Models.Entities
{
    /// <summary>
    /// Tài khoản thanh toán
    /// </summary>
    public class PaymentAccount : BaseEntity<Guid>
    {
        public string? Name { get; set; }
        public decimal? InitialMoney { get; set; }
        public string? Description { get; set; }
        public bool? IsReport { get; set; }
        public string? Icon { get; set; }
        public Guid? CurrencyId { get; set; }
        public Currency? Currency { get; set; }
        public Guid? TypeId { get; set; }
        public PaymentAccountType? Type { get; set; }
    }
}
