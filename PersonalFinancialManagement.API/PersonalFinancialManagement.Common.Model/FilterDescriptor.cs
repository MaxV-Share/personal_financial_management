using PersonalFinancialManagement.Common.Models.Enums;

namespace PersonalFinancialManagement.Common.Models
{
    public class FilterDescriptor
    {
        public string? Field { get; set; }
        public string?[]? Values { get; set; }
        public FilterType Operator { get; set; }
        public FilterLogicalOperator LogicalOperator { get; set; }
    }
}
