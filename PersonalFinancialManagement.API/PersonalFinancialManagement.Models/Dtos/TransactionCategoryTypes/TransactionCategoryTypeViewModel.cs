using PersonalFinancialManagement.Common.Models.DTOs;

namespace PersonalFinancialManagement.Models.Dtos.TransactionCategoryTypes
{
    public class TransactionCategoryTypeViewModel : BaseViewModel<Guid>, IBaseTransactionCategoryType
    {
        public string? Code { get; set; }
        public string? Name { get; set; }
    }
}
