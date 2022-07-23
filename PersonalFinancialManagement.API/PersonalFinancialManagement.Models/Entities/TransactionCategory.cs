using PersonalFinancialManagement.Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PersonalFinancialManagement.Models.Entities
{
    public class TransactionCategory : BaseEntity<Guid>
    {
        public string? Name { get; set; }
        public string? Description { get; set; }
        public string? Icon { get; set; }
        public TransactionCategory? Parent { get; set; }
    }
}
