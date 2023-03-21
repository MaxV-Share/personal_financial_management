using PersonalFinancialManagement.Common.Models;

namespace PersonalFinancialManagement.Models.Entities
{
    /// <summary>
    /// Loại tài khoản thanh toán
    /// </summary>
    public class PaymentAccountType : BaseEntity<Guid>
    {
        public string? Code { get; set; }
        public string? Name { get; set; }
    }
}
