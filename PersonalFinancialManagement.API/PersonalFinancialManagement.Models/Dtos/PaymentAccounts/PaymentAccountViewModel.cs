using Newtonsoft.Json;
using PersonalFinancialManagement.Common.Models.DTOs;

namespace PersonalFinancialManagement.Models.Dtos.PaymentAccounts
{
    public class PaymentAccountViewModel : BaseViewModel<Guid>, IBasePaymentAccountDto
    {
        [JsonProperty(PropertyName = "test")]
        public string? Name { get; set; }
        public decimal? InitialMoney { get; set; } = 0;
        public string? Description { get; set; }
        public bool? IsReport { get; set; }
        public string? Icon { get; set; }
        public Guid? CurrencyId { get; set; }
        public Guid? TypeId { get; set; }
    }
}
