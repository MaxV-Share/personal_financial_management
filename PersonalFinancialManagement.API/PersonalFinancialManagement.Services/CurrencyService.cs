using AutoMapper;
using Microsoft.Extensions.Logging;
using PersonalFinancialManagement.Common.Extensions;
using PersonalFinancialManagement.Common.Models;
using PersonalFinancialManagement.Common.Models.DTOs;
using PersonalFinancialManagement.EFCore;
using PersonalFinancialManagement.Models.DbContexts;
using PersonalFinancialManagement.Models.Dtos.Currencies;
using PersonalFinancialManagement.Models.Dtos.Currencies.Requests;
using PersonalFinancialManagement.Models.Entities;
using PersonalFinancialManagement.Repositories.UnitOffWorks;
using PersonalFinancialManagement.Services.Base;
using PersonalFinancialManagement.Services.Interfaces;

namespace PersonalFinancialManagement.Services;

public class CurrencyService :
    BaseService<ApplicationDbContext, Currency, CurrencyCreateRequest, CurrencyUpdateRequest,
        CurrencyViewModel, Guid>, ICurrencyService
{
    public CurrencyService(IMapper mapper, IUnitOffWork<ApplicationDbContext> unitOffWork,
        ILogger<CurrencyService> logger) : base(mapper, unitOffWork, logger)
    {
    }

    public override async Task<CurrencyViewModel?> CreateAsync(CurrencyCreateRequest? request)
    {
        // improve this method
        if (request == null)
            return null;

        CurrencyViewModel? result = null;
        await _unitOffWork.DoWorkWithTransaction(async () =>
        {
            var entity = _mapper.Map<Currency>(request);

            var countAffect = await _unitOffWork.Repository<Currency, Guid>().CreateAsync(entity);
            if (countAffect == 0) result = null;

            result = _mapper.Map<CurrencyViewModel>(entity);
        });

        return result;
    }

    public override async Task<CurrencyViewModel?> GetByIdAsync(Guid id)
    {
        var currency = await _unitOffWork.Repository<Currency, Guid>().GetByIdNoTrackingAsync(id);
        var result = _mapper.Map<CurrencyViewModel>(currency);
        return result;
    }
    // write docs for this method

    public async Task<IBasePaging<CurrencyViewModel>?> GetPagingAsync(IFilterBodyRequest request)
    {
        var query =
            _mapper.ProjectTo<CurrencyViewModel>(_unitOffWork.Repository<Currency, Guid>()
                .GetNoTrackingEntities());

        if (!request.SearchValue.IsNullOrEmpty())
            query = query.Where(e => e.Name!.Contains(request.SearchValue ?? ""));

        return await query.ToPagingAsync(request);
    }

    // improve GetPagingAsync method in new GetNewPagingAsync
    public async Task<IBasePaging<CurrencyViewModel>?> GetNewPagingAsync(
        IFilterBodyRequest request)
    {
        var query =
            _mapper.ProjectTo<CurrencyViewModel>(_unitOffWork.Repository<Currency, Guid>()
                .GetNoTrackingEntities());

        if (!request.SearchValue.IsNullOrEmpty())
            query = query.Where(e => e.Name!.Contains(request.SearchValue ?? ""));

        return await query.ToPagingAsync(request);
    }
}