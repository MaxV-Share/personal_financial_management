using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PersonalFinancialManagement.Services.Excels.Entities
{
    public class Transaction
    {
        public int? No { get; set; }
        public DateTime? TransactionDate { get; set; }
        public decimal? Money { get; set; }
        public decimal? RemainingBalance { get; set; }
        public string? ParentCategory { get; set; }
        public string? Category { get; set; }
        public string? SpendMoneyFor { get; set; }
        public string? Event { get; set; }
        public string? Description { get; set; }
    }
}
