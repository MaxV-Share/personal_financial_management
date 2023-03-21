using PersonalFinancialManagement.Common.Models.DTOs;
using PersonalFinancialManagement.Common.Models;
using PersonalFinancialManagement.Services.Base;
using PersonalFinancialManagement.Models.Entities;
using PersonalFinancialManagement.Models.DbContexts;
using PersonalFinancialManagement.Models.Dtos.PaymentAccounts.Requests;
using PersonalFinancialManagement.Models.Dtos.PaymentAccounts;

namespace PersonalFinancialManagement.Services.Interfaces
{
    public interface IPaymentAccountService : IBaseService<ApplicationDbContext, PaymentAccount, PaymentAccountCreateRequest, PaymentAccountUpdateRequest, PaymentAccountViewModel, Guid>
    {
        Task<IBasePaging<PaymentAccountViewModel>?> GetPagingAsync(IFilterBodyRequest request);
    }
}
