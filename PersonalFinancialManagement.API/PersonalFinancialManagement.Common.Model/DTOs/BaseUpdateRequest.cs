namespace PersonalFinancialManagement.Common.Models.DTOs
{
    public abstract class BaseUpdateRequest<TKey> : BaseDTO
    {
        public virtual TKey? Id { get; set; }
    }
}
