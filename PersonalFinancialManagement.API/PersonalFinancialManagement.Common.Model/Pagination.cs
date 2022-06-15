using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PersonalFinancialManagement.Common.Models
{
    public class Pagination
    {
        public Pagination()
        {
            PageIndex = 1;
            PageSize = 10;
            TotalRow = 0;
            PageCount = 0;
        }
        public int TotalRow { get; set; }
        public int PageCount { get; set; }
        public int PageIndex { get; set; }
        public int PageSize { get; set; }
    }
}
