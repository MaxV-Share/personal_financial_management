﻿using PersonalFinancialManagement.Common.Models.DTOs;

namespace PersonalFinancialManagement.Models.Dtos.PaymentAccounts.Requests
{
    public class PaymentAccountUpdateRequest: BaseUpdateRequest<Guid>, IBasePaymentAccountDto
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
