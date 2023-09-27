using AutoMapper;
using Microsoft.Extensions.Logging;
using PersonalFinancialManagement.Common.Models;
using PersonalFinancialManagement.Common.Models.DTOs;
using PersonalFinancialManagement.EFCore;
using PersonalFinancialManagement.Models.DbContexts;
using PersonalFinancialManagement.Models.Dtos.Transactions;
using PersonalFinancialManagement.Models.Dtos.Transactions.Requests;
using PersonalFinancialManagement.Models.Entities;
using PersonalFinancialManagement.Repositories.UnitOffWorks;
using PersonalFinancialManagement.Services.Base;
using PersonalFinancialManagement.Services.Interfaces;

namespace PersonalFinancialManagement.Services;

public class TransactionService :
    BaseService<ApplicationDbContext, Transaction, TransactionCreateRequest,
        TransactionUpdateRequest, TransactionViewModel, Guid>, ITransactionService
{
    public TransactionService(IMapper mapper, IUnitOffWork<ApplicationDbContext> unitOffWork,
        ILogger<TransactionService> logger) : base(mapper, unitOffWork, logger)
    {
    }

    public override async Task<TransactionViewModel?> CreateAsync(TransactionCreateRequest request)
    {

        var entity = _mapper.Map<Transaction>(request);

        var countAffect = await _unitOffWork.Repository<Transaction, Guid>().CreateAsync(entity);
        if (countAffect == 0)
        {
            return null;
        }

        var result = _mapper.Map<TransactionViewModel>(entity);


        return result;
    }

    public async Task<IBasePaging<TransactionViewModel>?> GetPagingAsync(IFilterBodyRequest request)
    {
        var query = _mapper.ProjectTo<TransactionViewModel>(_unitOffWork.Repository<Transaction, Guid>().GetNoTrackingEntities());

        return await query.ToPagingAsync(request);
    }
}