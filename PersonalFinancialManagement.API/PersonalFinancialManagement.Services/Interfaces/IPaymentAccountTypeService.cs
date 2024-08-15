using PersonalFinancialManagement.Common.Models;
using PersonalFinancialManagement.Common.Models.DTOs;
using PersonalFinancialManagement.Models.DbContexts;
using PersonalFinancialManagement.Models.Dtos.TransactionCategoryType;
using PersonalFinancialManagement.Models.Dtos.TransactionCategoryType.Requests;
using PersonalFinancialManagement.Models.Entities;
using PersonalFinancialManagement.Services.Base;

namespace PersonalFinancialManagement.Services.Interfaces;

public interface IPaymentAccountTypeService : IBaseService<ApplicationDbContext, PaymentAccountType,
    PaymentAccountTypeCreateRequest, PaymentAccountTypeUpdateRequest, PaymentAccountTypeViewModel,
    Guid>
{
    Task<IBasePaging<PaymentAccountTypeViewModel>?> GetPagingAsync(IFilterBodyRequest request);
}