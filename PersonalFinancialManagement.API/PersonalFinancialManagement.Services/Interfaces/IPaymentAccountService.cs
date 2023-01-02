using PersonalFinancialManagement.Common.Models.DTOs;
using PersonalFinancialManagement.Common.Models;
using PersonalFinancialManagement.Services.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PersonalFinancialManagement.Models.Entities;
using PersonalFinancialManagement.Models.DbContexts;
using PersonalFinancialManagement.Models.Dtos.Currencies;
using PersonalFinancialManagement.Models.Dtos.Currencies.Requests;
using PersonalFinancialManagement.Models.Dtos.PaymentAccounts.Requests;
using PersonalFinancialManagement.Models.Dtos.PaymentAccounts;

namespace PersonalFinancialManagement.Services.Interfaces
{
    public interface IPaymentAccountService : IBaseService<ApplicationDbContext, PaymentAccount, PaymentAccountCreateRequest, PaymentAccountUpdateRequest, PaymentAccountViewModel, Guid>
    {
        Task<IBasePaging<PaymentAccountViewModel>?> GetPagingAsync(IFilterBodyRequest request);
    }
}
