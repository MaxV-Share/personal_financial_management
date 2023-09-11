using PersonalFinancialManagement.Common.Models.DTOs;
using PersonalFinancialManagement.Common.Models;
using PersonalFinancialManagement.Models.DbContexts;
using PersonalFinancialManagement.Models.Dtos.TransactionCategories;
using PersonalFinancialManagement.Models.Dtos.TransactionCategories.Requests;
using PersonalFinancialManagement.Models.Entities;
using PersonalFinancialManagement.Services.Base;

namespace PersonalFinancialManagement.Services.Interfaces;

public interface ITransactionCategoryService : IBaseService<ApplicationDbContext, TransactionCategory,
    TransactionCategoryCreateRequest, TransactionCategoryUpdateRequest, TransactionCategoryViewModel, Guid>
{
    Task<IBasePaging<TransactionCategoryViewModel>?> GetPagingAsync(IFilterBodyRequest request);
}