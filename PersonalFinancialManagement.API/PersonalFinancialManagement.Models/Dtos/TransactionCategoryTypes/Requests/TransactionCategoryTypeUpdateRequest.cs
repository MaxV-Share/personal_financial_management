using PersonalFinancialManagement.Common.Models.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PersonalFinancialManagement.Models.Dtos.TransactionCategoryTypes;

namespace PersonalFinancialManagement.Models.Dtos.TransactionCategoryTypes.Requests
{
    public class TransactionCategoryTypeUpdateRequest : BaseUpdateRequest<Guid>, IBaseTransactionCategoryType
    {
        public string? Code { get; set; }
        public string? Name { get; set; }
    }
}
