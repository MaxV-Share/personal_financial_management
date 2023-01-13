using PFM;
using PFM.Common.Models;
using PFM.Common.Models.Enums;

namespace PFM.Common.Models
{
    public class FilterDescriptor
    {
        public string? Field { get; set; }
        public string[]? Values { get; set; }
        public FilterType Operator { get; set; }
        public FilterLogicalOperator LogicalOperator { get; set; }
    }
}
