using PersonalFinancialManagement.Common.Models.DTOs;

namespace PersonalFinancialManagement.Models.Dtos.Currencies
{
    public class CurrencyViewModel : BaseViewModel<Guid>, IBaseCurrencyModel
    {
        public string? Code { get; set; }
        public string? Name { get; set; }
        public string? Icon { get; set; }
    }
}
