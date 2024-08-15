using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using PersonalFinancialManagement.Common.Extensions;
using PersonalFinancialManagement.Common.Models;
using PersonalFinancialManagement.Common.Models.DTOs;
using PersonalFinancialManagement.EFCore;
using PersonalFinancialManagement.Models.DbContexts;
using PersonalFinancialManagement.Models.Dtos.PaymentAccounts;
using PersonalFinancialManagement.Models.Dtos.PaymentAccounts.Requests;
using PersonalFinancialManagement.Models.Entities;
using PersonalFinancialManagement.Repositories.UnitOffWorks;
using PersonalFinancialManagement.Services.Base;
using PersonalFinancialManagement.Services.Interfaces;

namespace PersonalFinancialManagement.Services;

public class PaymentAccountService(
    IMapper mapper,
    IUnitOffWork<ApplicationDbContext> unitOffWork,
    ILogger<PaymentAccountService> logger)
    :
        BaseService<ApplicationDbContext, PaymentAccount, PaymentAccountCreateRequest,
            PaymentAccountUpdateRequest, PaymentAccountViewModel,
            Guid>(mapper, unitOffWork, logger), IPaymentAccountService
{
    public override async Task<PaymentAccountViewModel?> CreateAsync(
        PaymentAccountCreateRequest? request)
    {
        if (request == null)
            return null;

        PaymentAccountViewModel? result = null;
        await _unitOffWork.DoWorkWithTransaction(async () =>
        {
            var entity = _mapper.Map<PaymentAccount>(request);

            var countAffect =
                await _unitOffWork.Repository<PaymentAccount, Guid>().CreateAsync(entity);
            if (countAffect == 0)
            {
                result = null;
                return;
            }

            result = _mapper.Map<PaymentAccountViewModel>(entity);
        });

        return result;
    }

    public override async Task<PaymentAccountViewModel?> GetByIdAsync(Guid id)
    {
        var paymentAccount = await _unitOffWork.Repository<PaymentAccount, Guid>()
            .GetByIdNoTrackingAsync(id);
        var result = _mapper.Map<PaymentAccountViewModel>(paymentAccount);
        return result;
    }

    public async Task<IBasePaging<PaymentAccountViewModel>?> GetPagingAsync(
        IFilterBodyRequest request)
    {
        var query = _mapper.ProjectTo<PaymentAccountViewModel>(_unitOffWork
            .Repository<PaymentAccount, Guid>().GetNoTrackingEntities());


        if (!request.SearchValue.IsNullOrEmpty())
            query = query.Where(e => e.Name!.Contains(request.SearchValue ?? ""));
        var queryText = query.ToQueryString();

        return await query.ToPagingAsync(request);
    }
}