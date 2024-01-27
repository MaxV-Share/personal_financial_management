using AutoMapper;
using Microsoft.Extensions.Logging;
using PersonalFinancialManagement.Common.Models;
using PersonalFinancialManagement.Common.Models.DTOs;
using PersonalFinancialManagement.EFCore;
using PersonalFinancialManagement.Models.DbContexts;
using PersonalFinancialManagement.Models.Dtos.TransactionCategories;
using PersonalFinancialManagement.Models.Dtos.TransactionCategories.Requests;
using PersonalFinancialManagement.Models.Entities;
using PersonalFinancialManagement.Repositories.UnitOffWorks;
using PersonalFinancialManagement.Services.Base;
using PersonalFinancialManagement.Services.Interfaces;

namespace PersonalFinancialManagement.Services;

public class TransactionCategoryService(
    IMapper mapper,
    IUnitOffWork<ApplicationDbContext> unitOffWork,
    ILogger<TransactionCategoryService> logger)
    :
        BaseService<ApplicationDbContext, TransactionCategory, TransactionCategoryCreateRequest,
            TransactionCategoryUpdateRequest, TransactionCategoryViewModel, Guid>(mapper,
            unitOffWork, logger),
        ITransactionCategoryService
{
    public override Task<TransactionCategoryViewModel?> CreateAsync(
        TransactionCategoryCreateRequest request)
    {
        return base.CreateAsync(request);
    }

    public async Task<IBasePaging<TransactionCategoryViewModel>?> GetPagingAsync(
        IFilterBodyRequest request)
    {
        var query = _mapper.ProjectTo<TransactionCategoryViewModel>(_unitOffWork
            .Repository<TransactionCategory, Guid>()
            .GetNoTrackingEntities());

        return await query.ToPagingAsync(request);
    }
}