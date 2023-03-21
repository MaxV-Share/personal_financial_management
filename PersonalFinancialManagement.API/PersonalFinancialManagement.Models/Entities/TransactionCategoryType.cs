using PersonalFinancialManagement.Common.Models;

namespace PersonalFinancialManagement.Models.Entities
{
    public class TransactionCategoryType : BaseEntity<Guid>
    {
        public string? Code { get; set; }
        public string? Name { get; set; }
    }
}
