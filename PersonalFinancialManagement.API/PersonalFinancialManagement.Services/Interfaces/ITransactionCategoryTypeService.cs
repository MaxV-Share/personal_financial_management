using PersonalFinancialManagement.Common.Models;
using PersonalFinancialManagement.Common.Models.DTOs;
using PersonalFinancialManagement.Models.DbContexts;
using PersonalFinancialManagement.Models.Dtos.TransactionCategoryTypes;
using PersonalFinancialManagement.Models.Dtos.TransactionCategoryTypes.Requests;
using PersonalFinancialManagement.Models.Entities;
using PersonalFinancialManagement.Services.Base;

namespace PersonalFinancialManagement.Services.Interfaces;

public interface ITransactionCategoryTypeService : IBaseService<ApplicationDbContext,
    TransactionCategoryType, TransactionCategoryTypeCreateRequest,
    TransactionCategoryTypeUpdateRequest, TransactionCategoryTypeViewModel, Guid>
{
    Task<IBasePaging<TransactionCategoryTypeViewModel>?> GetPagingAsync(IFilterBodyRequest request);
}