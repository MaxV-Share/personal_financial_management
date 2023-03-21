namespace PersonalFinancialManagement.Models.Dtos.Currencies
{
    public interface IBaseCurrencyModel
    {
        public string? Code { get; set; }
        public string? Name { get; set; }
        public string? Icon { get; set; }
    }
}
