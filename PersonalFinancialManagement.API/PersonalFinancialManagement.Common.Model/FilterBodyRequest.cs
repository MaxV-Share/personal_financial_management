using System;
using System.Collections.Generic;
using System.Text;
using PersonalFinancialManagement.Common.Models;

namespace PersonalFinancialManagement.Common.Models
{
    public class FilterBodyRequest : BodyRequest, IFilterBodyRequest
    {
        public string LangId { get; set; }
        public string SearchValue { get; set; }
        public FilterRequest Filter { get; set; }
        public IEnumerable<SortDescriptor> Orders { get; set; }
        public Pagination Pagination { get; set; }
    }
    public interface IFilterBodyRequest : IBodyRequest
    {
        public string LangId { get; set; }
        public string SearchValue { get; set; }
        public FilterRequest Filter { get; set; }
        public IEnumerable<SortDescriptor> Orders { get; set; }
        public Pagination Pagination { get; set; }
    }
}
