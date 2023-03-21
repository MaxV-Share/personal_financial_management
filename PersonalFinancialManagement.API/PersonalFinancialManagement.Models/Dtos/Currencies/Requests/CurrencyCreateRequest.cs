using PersonalFinancialManagement.Common.Models.DTOs;

namespace PersonalFinancialManagement.Models.Dtos.Currencies.Requests
{
    public class CurrencyCreateRequest : BaseCreateRequest, IBaseCurrencyModel
    {
        public string? Code { get; set; }
        public string? Name { get; set; }
        public string? Icon { get; set; }
    }
}
