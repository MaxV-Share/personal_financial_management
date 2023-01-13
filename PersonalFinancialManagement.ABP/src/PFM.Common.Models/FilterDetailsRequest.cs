using PFM;
using PFM.Common.Models.Enums;

namespace PFM.Common.Models
{
    public class FilterDetailsRequest
    {
        public string? AttributeName { get; set; }
        public string? Value { get; set; }
        public FilterType FilterType { get; set; }
    }
}
