using PersonalFinancialManagement.Common.Models.DTOs;
using PersonalFinancialManagement.Common.Models;
using PersonalFinancialManagement.Services.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PersonalFinancialManagement.Models.Entities;
using PersonalFinancialManagement.Models.Dtos.TransactionCategoryType.Requests;
using PersonalFinancialManagement.Models.Dtos.TransactionCategoryType;
using PersonalFinancialManagement.Models.DbContexts;

namespace PersonalFinancialManagement.Services.Interfaces
{
    public interface ITransactionCategoryTypeService : IBaseService<ApplicationDbContext, TransactionCategoryType, TransactionCategoryTypeCreateRequest, TransactionCategoryTypeUpdateRequest, TransactionCategoryTypeViewModel, Guid>
    {
        Task<IBasePaging<TransactionCategoryTypeViewModel>?> GetPagingAsync(IFilterBodyRequest request);
    }
}
