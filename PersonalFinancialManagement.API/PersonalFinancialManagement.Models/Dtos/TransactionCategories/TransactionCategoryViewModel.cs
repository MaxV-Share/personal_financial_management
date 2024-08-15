using PersonalFinancialManagement.Common.Models.DTOs;

namespace PersonalFinancialManagement.Models.Dtos.TransactionCategories;

public class TransactionCategoryViewModel : BaseViewModel<Guid>, IBaseTransactionCategory
{
    public string? Name { get; set; }
    public string? Description { get; set; }
    public string? Icon { get; set; }
    public Guid? ParentId { get; set; }
    public string? ParentName { get; set; }
}