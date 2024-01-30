using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using PersonalFinancialManagement.Common;
using PersonalFinancialManagement.Common.Models;
using PersonalFinancialManagement.Common.Models.DTOs;
using PersonalFinancialManagement.Repositories.UnitOffWorks;

namespace PersonalFinancialManagement.Services.Base;

/// <summary>
/// </summary>
/// <typeparam name="TEntity"></typeparam>
/// <typeparam name="TCreateRequest"></typeparam>
/// <typeparam name="TUpdateRequest"></typeparam>
/// <typeparam name="TViewModel"></typeparam>
/// <typeparam name="TKey"></typeparam>
/// <typeparam name="TContext"></typeparam>
public abstract class BaseService<TContext, TEntity, TCreateRequest, TUpdateRequest, TViewModel,
    TKey>(
    IMapper mapper,
    IUnitOffWork<TContext> unitOffWork,
    ILogger logger
)
    : IBaseService<TContext, TEntity, TCreateRequest, TUpdateRequest, TViewModel, TKey>
    where TEntity : BaseEntity<TKey>, new()
    where TCreateRequest : BaseCreateRequest, new()
    where TUpdateRequest : BaseUpdateRequest<TKey>, new()
    where TViewModel : BaseViewModel<TKey>, new()
    where TContext : DbContext
{
    protected readonly ILogger _logger = logger;
    protected readonly IMapper _mapper = mapper;
    protected readonly IUnitOffWork<TContext> _unitOffWork = unitOffWork;

    /// <summary>
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    public async Task<int?> DeleteHardAsync(TKey id)
    {
        _logger.LogTrace($"DeleteHardAsync: {id.TryParseToString()}");
        await _unitOffWork.Repository<TEntity, TKey>().DeleteHardAsync(id!);
        return await _unitOffWork.SaveChangesAsync();
    }

    /// <summary>
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    public async Task<int?> DeleteSoftAsync(TKey id)
    {
        _logger.LogTrace($"DeleteSoftAsync: {id.TryParseToString()}");
        return await _unitOffWork.Repository<TEntity, TKey>().DeleteSoftAsync(id!);
    }

    /// <summary>
    /// </summary>
    /// <returns></returns>
    public virtual async Task<IEnumerable<TViewModel>?> GetAllDtoAsync()
    {
        var query = _unitOffWork.Repository<TEntity, TKey>();
        var result = await _mapper.ProjectTo<TViewModel>(query.GetNoTrackingEntities())
            .ToListAsync();
        _logger.LogTrace($"GetAllDtoAsync result: {result.TryParseToString()}");
        return result;
    }

    public virtual async Task<TViewModel?> GetByIdAsync(TKey id)
    {
        _logger.LogTrace($"GetByIdAsync: {id.TryParseToString()}");
        var entity = await _unitOffWork.Repository<TEntity, TKey>().GetByIdNoTrackingAsync(id);
        var result = _mapper.Map<TViewModel>(entity);
        _logger.LogTrace($"GetByIdAsync result: {result.TryParseToString()}");
        return result;
    }

    public virtual async Task<TViewModel?> UpdateAsync(TKey id, TUpdateRequest request)
    {
        _logger.LogTrace($"UpdateAsync request: {id}, {request.TryParseToString()}");
        if (id is null || !id.Equals(request.Id))
            throw new KeyNotFoundException();
        var entity = await _unitOffWork.Repository<TEntity, TKey>().GetByIdAsync(id);
        _logger.LogTrace($"UpdateAsync old entity: {entity.TryParseToString()}");

        if (entity == null) throw new NullReferenceException();
        entity = _mapper.Map(request, entity);
        _logger.LogTrace($"UpdateAsync new entity: {entity.TryParseToString()}");
        var effectedCount = await _unitOffWork.Repository<TEntity, TKey>().UpdateAsync(entity);
        _logger.LogTrace($"UpdateAsync effectedCount: {effectedCount}");
        if (effectedCount <= 0) throw new NullReferenceException();
        var result = _mapper.Map<TViewModel>(entity);
        _logger.LogTrace($"UpdateAsync result: {result.TryParseToString()}");
        return result;
    }

    /// <summary>
    /// </summary>
    /// <param name="request"></param>
    /// <returns></returns>
    /// <exception cref="NullReferenceException"></exception>
    public virtual async Task<TViewModel?> CreateAsync(TCreateRequest request)
    {
        return await _unitOffWork.DoWorkWithTransaction(async () =>
        {
            _logger.LogTrace($"CreateAsync request: {request.TryParseToString()}");
            var entityNew = new TEntity();
            _mapper.Map(request, entityNew);
            _logger.LogTrace($"CreateAsync entitiesNew: {entityNew.TryParseToString()}");
            var effectedCount =
                await _unitOffWork.Repository<TEntity, TKey>().CreateAsync(entityNew);
            _logger.LogTrace($"CreateAsync affectedCount: {effectedCount}");
            if (effectedCount <= 0) throw new NullReferenceException();
            var result = _mapper.Map<TViewModel>(entityNew);
            _logger.LogTrace($"CreateAsync result: {result.TryParseToString()}");
            return result;
        });
    }

    /// <summary>
    /// </summary>
    /// <param name="request"></param>
    /// <returns></returns>
    /// <exception cref="NullReferenceException"></exception>
    public virtual async Task<IEnumerable<TViewModel>?> CreateAsync(
        List<TCreateRequest> request)
    {
        if (!request.Any())
        {
            _logger.LogInformation("Request Is Empty");
            return new List<TViewModel>();
        }

        return await _unitOffWork.DoWorkWithTransaction(async () =>
        {
            var baseCreateRequests = request as TCreateRequest[] ?? request.ToArray();
            _logger.LogTrace($"CreateAsync request: {baseCreateRequests.TryParseToString()}");

            var entitiesNew = new List<TEntity>();
            _mapper.Map(baseCreateRequests, entitiesNew);
            _logger.LogTrace($"CreateAsync entitiesNew: {entitiesNew.TryParseToString()}");

            var effectedCount =
                await _unitOffWork.Repository<TEntity, TKey>().CreateAsync(entitiesNew);
            _logger.LogTrace($"CreateAsync affectedCount: {effectedCount}");

            if (effectedCount <= 0) throw new NullReferenceException();

            var result = _mapper.Map<IEnumerable<TViewModel>>(entitiesNew);
            var baseViewModels = result as TViewModel[] ?? result.ToArray();
            _logger.LogTrace($"CreateAsync result: {baseViewModels.TryParseToString()}");
            return baseViewModels;
        });
    }
}