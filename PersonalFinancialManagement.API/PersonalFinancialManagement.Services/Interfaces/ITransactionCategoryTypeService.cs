using PersonalFinancialManagement.Common.Models.DTOs;
using PersonalFinancialManagement.Common.Models;
using PersonalFinancialManagement.Services.Base;
using PersonalFinancialManagement.Models.Entities;
using PersonalFinancialManagement.Models.DbContexts;
using PersonalFinancialManagement.Models.Dtos.TransactionCategoryTypes;
using PersonalFinancialManagement.Models.Dtos.TransactionCategoryTypes.Requests;

namespace PersonalFinancialManagement.Services.Interfaces
{
    public interface ITransactionCategoryTypeService : IBaseService<ApplicationDbContext, TransactionCategoryType, TransactionCategoryTypeCreateRequest, TransactionCategoryTypeUpdateRequest, TransactionCategoryTypeViewModel, Guid>
    {
        Task<IBasePaging<TransactionCategoryTypeViewModel>?> GetPagingAsync(IFilterBodyRequest request);
    }
}
