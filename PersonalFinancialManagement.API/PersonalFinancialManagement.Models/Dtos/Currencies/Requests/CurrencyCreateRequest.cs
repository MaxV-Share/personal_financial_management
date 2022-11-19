using PersonalFinancialManagement.Common.Models.DTOs;
using PersonalFinancialManagement.Models.Dtos.Currencies;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PersonalFinancialManagement.Models.Dtos.Currencies.Requests
{
    public class CurrencyCreateRequest : BaseCreateRequest, IBaseCurrencyModel
    {
        public string? Code { get; set; }
        public string? Name { get; set; }
        public string? Icon { get; set; }
    }
}
