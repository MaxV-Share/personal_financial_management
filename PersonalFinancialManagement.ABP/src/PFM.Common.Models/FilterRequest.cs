using System.Collections.Generic;
using PFM.Common.Models;
using PFM;

namespace PFM.Common.Models
{
    public class FilterRequest
    {
        public FilterLogicalOperator LogicalOperator { get; set; }
        public IEnumerable<FilterDetailsRequest>? Details { get; set; }
    }
}
