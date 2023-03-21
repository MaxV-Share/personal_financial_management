using PersonalFinancialManagement.Common.Models;

namespace PersonalFinancialManagement.Models.Entities
{
    /// <summary>
    /// Tiền tệ
    /// </summary>
    public class Currency : BaseEntity<Guid>
    {
        public string? Code { get; set; }
        public string? Name { get; set; }
        public string? Icon { get; set; }
    }
}
