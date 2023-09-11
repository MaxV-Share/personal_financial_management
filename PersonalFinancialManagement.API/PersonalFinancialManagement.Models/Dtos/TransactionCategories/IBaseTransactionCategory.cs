namespace PersonalFinancialManagement.Models.Dtos.TransactionCategories;

public interface IBaseTransactionCategory
{
    public string? Name { get; set; }
    public string? Description { get; set; }
    public string? Icon { get; set; }
    public Guid? ParentId { get; set; }
}