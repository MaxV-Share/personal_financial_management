using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PersonalFinancialManagement.Common.Models.Enums;

namespace PersonalFinancialManagement.Common.Models
{
    public class SortDescriptor
    {
        public string Field { get; set; }
        public SortDirection Direction { get; set; }
    }
}
