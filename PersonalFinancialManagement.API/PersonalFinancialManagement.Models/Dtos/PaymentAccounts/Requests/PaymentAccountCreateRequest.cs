using PersonalFinancialManagement.Common.Models.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PersonalFinancialManagement.Models.Dtos.PaymentAccounts.Requests
{
    public class PaymentAccountCreateRequest : BaseCreateRequest, IBasePaymentAccountDto
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
