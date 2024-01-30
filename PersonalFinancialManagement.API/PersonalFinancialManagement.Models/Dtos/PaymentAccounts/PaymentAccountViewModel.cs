using PersonalFinancialManagement.Common.Models.DTOs;

namespace PersonalFinancialManagement.Models.Dtos.PaymentAccounts;

public class PaymentAccountViewModel : BaseViewModel<Guid>,
    IBasePaymentAccountDto
{
    public string? Name { get; set; }
    public decimal? InitialMoney { get; set; } = 0;
    public string? Description { get; set; }
    public bool? IsReport { get; set; }
    public string? Icon { get; set; }
    public Guid? CurrencyId { get; set; }
    public Guid? TypeId { get; set; }
}