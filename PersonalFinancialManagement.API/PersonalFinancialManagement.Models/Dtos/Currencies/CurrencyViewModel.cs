using PersonalFinancialManagement.Common.Models.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PersonalFinancialManagement.Models.Dtos.Currencies
{
    public class CurrencyViewModel : BaseViewModel<Guid>, IBaseCurrencyModel
    {
        public string? Code { get; set; }
        public string? Name { get; set; }
        public string? Icon { get; set; }
    }
}
