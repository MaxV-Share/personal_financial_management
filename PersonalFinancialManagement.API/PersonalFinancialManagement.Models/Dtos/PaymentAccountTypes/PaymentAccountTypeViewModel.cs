using PersonalFinancialManagement.Common.Models.DTOs;

namespace PersonalFinancialManagement.Models.Dtos.TransactionCategoryType
{
    public class PaymentAccountTypeViewModel : BaseViewModel<Guid>, IBasePaymentAccountType
    {
        public string? Code { get; set; }
        public string? Name { get; set; }
    }
}
