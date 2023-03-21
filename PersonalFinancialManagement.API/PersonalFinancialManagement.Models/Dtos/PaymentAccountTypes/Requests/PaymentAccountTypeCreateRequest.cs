using PersonalFinancialManagement.Common.Models.DTOs;

namespace PersonalFinancialManagement.Models.Dtos.TransactionCategoryType.Requests
{
    public class PaymentAccountTypeCreateRequest : BaseCreateRequest, IBasePaymentAccountType
    {
        public string? Code { get; set; }
        public string? Name { get; set; }
    }
}
