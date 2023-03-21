using PersonalFinancialManagement.Common.Models.DTOs;

namespace PersonalFinancialManagement.Models.Dtos.TransactionCategoryTypes.Requests
{
    public class TransactionCategoryTypeCreateRequest : BaseCreateRequest, IBaseTransactionCategoryType
    {
        public string? Code { get; set; }
        public string? Name { get; set; }
    }
}
