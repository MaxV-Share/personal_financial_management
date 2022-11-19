﻿using PersonalFinancialManagement.Common.Models.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PersonalFinancialManagement.Models.Dtos.TransactionCategoryType.Requests
{
    public class PaymentAccountTypeCreateRequest : BaseCreateRequest, IBasePaymentAccountType
    {
        public string? Code { get; set; }
        public string? Name { get; set; }
    }
}