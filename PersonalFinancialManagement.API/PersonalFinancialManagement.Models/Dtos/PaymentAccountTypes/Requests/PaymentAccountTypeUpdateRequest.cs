using PersonalFinancialManagement.Common.Models.DTOs;

namespace PersonalFinancialManagement.Models.Dtos.TransactionCategoryType.Requests
{
    public class PaymentAccountTypeUpdateRequest : BaseUpdateRequest<Guid>, IBasePaymentAccountType
    {
        public string? Code { get; set; }
        public string? Name { get; set; }
    }
}
