using PersonalFinancialManagement.Common.Models.DTOs;
using PersonalFinancialManagement.Common.Models;
using PersonalFinancialManagement.Services.Base;
using PersonalFinancialManagement.Models.Entities;
using PersonalFinancialManagement.Models.DbContexts;
using PersonalFinancialManagement.Models.Dtos.TransactionCategoryType.Requests;
using PersonalFinancialManagement.Models.Dtos.TransactionCategoryType;

namespace PersonalFinancialManagement.Services.Interfaces
{
    public interface IPaymentAccountTypeService : IBaseService<ApplicationDbContext, PaymentAccountType, PaymentAccountTypeCreateRequest, PaymentAccountTypeUpdateRequest, PaymentAccountTypeViewModel, Guid>
    {
        Task<IBasePaging<PaymentAccountTypeViewModel>?> GetPagingAsync(IFilterBodyRequest request);
    }
}
