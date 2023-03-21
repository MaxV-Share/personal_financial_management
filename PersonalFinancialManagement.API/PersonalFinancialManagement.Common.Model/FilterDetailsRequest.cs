using PersonalFinancialManagement.Common.Models.Enums;

namespace PersonalFinancialManagement.Common.Models
{
    public class FilterDetailsRequest
    {
        public string? AttributeName { get; set; }
        public string? Value { get; set; }
        public FilterType FilterType { get; set; }
    }
}
