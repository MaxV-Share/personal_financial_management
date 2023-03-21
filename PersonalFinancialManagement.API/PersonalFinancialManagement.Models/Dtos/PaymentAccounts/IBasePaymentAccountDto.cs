namespace PersonalFinancialManagement.Models.Dtos.PaymentAccounts
{
    public interface IBasePaymentAccountDto
    {
        public string? Name { get; set; }
        public string? InitialMoney { get; set; }
        public string? Description { get; set; }
        public bool? IsReport { get; set; }
        public string? Icon { get; set; }
        public Guid? CurrencyId { get; set; }
        public Guid? TypeId { get; set; }
    }
}
