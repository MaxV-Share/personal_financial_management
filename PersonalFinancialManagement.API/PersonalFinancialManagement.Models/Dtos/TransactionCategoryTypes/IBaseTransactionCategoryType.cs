namespace PersonalFinancialManagement.Models.Dtos.TransactionCategoryTypes
{
    public interface IBaseTransactionCategoryType
    {
        public string? Code { get; set; }
        public string? Name { get; set; }
    }
}
