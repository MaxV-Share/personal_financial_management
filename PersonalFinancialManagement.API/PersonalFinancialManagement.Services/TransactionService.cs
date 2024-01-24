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

        if (request.CategoryId != null)
        {
            var transactionCategory = await _unitOffWork.Repository<TransactionCategory, Guid>()
                .GetByIdAsync(request.CategoryId!.Value);
            entity.Category = transactionCategory;
            entity.CategoryId = request.CategoryId;
        }

        if (request.FromPaymentAccountId != null)
        {
            var fromPaymentAccount = await _unitOffWork.Repository<PaymentAccount, Guid>()
                .GetByIdAsync(request.FromPaymentAccountId!.Value);
            entity.FromPaymentAccount = fromPaymentAccount;
            entity.FromPaymentAccountId = request.FromPaymentAccountId;
        }

        var countAffect = await _unitOffWork.Repository<Transaction, Guid>().CreateAsync(entity);
        if (countAffect == 0) return null;

        var result = _mapper.Map<TransactionViewModel>(entity);


        return result;
    }

    public override async Task<TransactionViewModel?> UpdateAsync(Guid id,
        TransactionUpdateRequest request)
    {
        var entity = _mapper.Map<Transaction>(request);
        TransactionViewModel? result = null;
        await _unitOffWork.DoWorkWithTransaction(async () =>
        {
            if (request.CategoryId != null)
            {
                var transactionCategory = await _unitOffWork.Repository<TransactionCategory, Guid>()
                    .GetByIdAsync(request.CategoryId!.Value);
                entity.Category = transactionCategory;
                entity.CategoryId = request.CategoryId;
            }

            if (request.FromPaymentAccountId != null)
            {
                var fromPaymentAccount = await _unitOffWork.Repository<PaymentAccount, Guid>()
                    .GetByIdAsync(request.FromPaymentAccountId!.Value);
                entity.FromPaymentAccount = fromPaymentAccount;
                entity.FromPaymentAccountId = request.FromPaymentAccountId;
            }

            var countAffect =
                await _unitOffWork.Repository<Transaction, Guid>().CreateAsync(entity);
            if (countAffect == 0) throw new ArgumentNullException();

            result = _mapper.Map<TransactionViewModel>(entity);
        });


        return result;
    }

    public async Task<IBasePaging<TransactionViewModel>?> GetPagingAsync(IFilterBodyRequest request)
    {
        var query =
            _mapper.ProjectTo<TransactionViewModel>(_unitOffWork.Repository<Transaction, Guid>()
                .GetNoTrackingEntities());
        var queryText = query.ToString();
        return await query.ToPagingAsync(request);
    }
}