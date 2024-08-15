using PersonalFinancialManagement.Common.Models;
using PersonalFinancialManagement.Common.Models.DTOs;
using PersonalFinancialManagement.Models.DbContexts;
using PersonalFinancialManagement.Models.Dtos.Transactions;
using PersonalFinancialManagement.Models.Dtos.Transactions.Requests;
using PersonalFinancialManagement.Models.Entities;
using PersonalFinancialManagement.Services.Base;

namespace PersonalFinancialManagement.Services.Interfaces;

public interface ITransactionService : IBaseService<ApplicationDbContext, Transaction,
    TransactionCreateRequest, TransactionUpdateRequest, TransactionViewModel, Guid>
{
    Task<IBasePaging<TransactionViewModel>?> GetPagingAsync(IFilterBodyRequest request);
}