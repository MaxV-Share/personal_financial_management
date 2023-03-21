namespace PersonalFinancialManagement.Common.Models
{
    public class FilterRequest
    {
        public FilterLogicalOperator LogicalOperator { get; set; }
        public IEnumerable<FilterDetailsRequest>? Details { get; set; }
    }
}
