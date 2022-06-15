using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PersonalFinancialManagement.Common.Models;

namespace PersonalFinancialManagement.Common.Models
{
    public class FilterRequest
    {
        public FilterLogicalOperator LogicalOperator { get; set; }
        public IEnumerable<FilterDetailsRequest> Details { get; set; }
    }
}
