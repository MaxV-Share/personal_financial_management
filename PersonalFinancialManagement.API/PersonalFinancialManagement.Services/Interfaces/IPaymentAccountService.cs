using PersonalFinancialManagement.Common.Models;
using PersonalFinancialManagement.Common.Models.DTOs;
using PersonalFinancialManagement.Models.DbContexts;
using PersonalFinancialManagement.Models.Dtos.PaymentAccounts;
using PersonalFinancialManagement.Models.Dtos.PaymentAccounts.Requests;
using PersonalFinancialManagement.Models.Entities;
using PersonalFinancialManagement.Services.Base;

namespace PersonalFinancialManagement.Services.Interfaces;

public interface IPaymentAccountService : IBaseService<ApplicationDbContext, PaymentAccount,
    PaymentAccountCreateRequest, PaymentAccountUpdateRequest, PaymentAccountViewModel, Guid>
{
    Task<IBasePaging<PaymentAccountViewModel>?> GetPagingAsync(IFilterBodyRequest request);
}