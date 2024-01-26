using AutoMapper;
using Microsoft.Extensions.Logging;
using PersonalFinancialManagement.Common.Extensions;
using PersonalFinancialManagement.Common.Models;
using PersonalFinancialManagement.Common.Models.DTOs;
using PersonalFinancialManagement.EFCore;
using PersonalFinancialManagement.Models.DbContexts;
using PersonalFinancialManagement.Models.Dtos.TransactionCategoryType;
using PersonalFinancialManagement.Models.Dtos.TransactionCategoryType.Requests;
using PersonalFinancialManagement.Models.Entities;
using PersonalFinancialManagement.Repositories.UnitOffWorks;
using PersonalFinancialManagement.Services.Base;
using PersonalFinancialManagement.Services.Interfaces;

namespace PersonalFinancialManagement.Services;

public class PaymentAccountTypeService :
    BaseService<ApplicationDbContext, PaymentAccountType, PaymentAccountTypeCreateRequest,
        PaymentAccountTypeUpdateRequest, PaymentAccountTypeViewModel, Guid>,
    IPaymentAccountTypeService
{
    public PaymentAccountTypeService(IMapper mapper, IUnitOffWork<ApplicationDbContext> unitOffWork,
        ILogger<PaymentAccountTypeService> logger) : base(mapper, unitOffWork, logger)
    {
    }

    public override async Task<PaymentAccountTypeViewModel?> CreateAsync(
        PaymentAccountTypeCreateRequest? request)
    {
        PaymentAccountTypeViewModel? result = null;
        await _unitOffWork.DoWorkWithTransaction(async () =>
        {
            var entity = _mapper.Map<PaymentAccountType>(request);

            var countAffect = await _unitOffWork.Repository<PaymentAccountType, Guid>()
                .CreateAsync(entity);
            if (countAffect != 0)
                result = _mapper.Map<PaymentAccountTypeViewModel>(entity);
        });

        return result;
    }

    public override async Task<PaymentAccountTypeViewModel?> GetByIdAsync(Guid id)
    {
        var paymentAccountType = await _unitOffWork.Repository<PaymentAccountType, Guid>()
            .GetByIdNoTrackingAsync(id);
        var result = _mapper.Map<PaymentAccountTypeViewModel>(paymentAccountType);
        return result;
    }

    public async Task<IBasePaging<PaymentAccountTypeViewModel>?> GetPagingAsync(
        IFilterBodyRequest request)
    {
        var query = _mapper.ProjectTo<PaymentAccountTypeViewModel>(_unitOffWork
            .Repository<PaymentAccountType, Guid>().GetNoTrackingEntities());


        if (!request.SearchValue.IsNullOrEmpty())
            query = query.Where(e => e.Name!.Contains(request.SearchValue!));

        return await query.ToPagingAsync(request);
    }
}