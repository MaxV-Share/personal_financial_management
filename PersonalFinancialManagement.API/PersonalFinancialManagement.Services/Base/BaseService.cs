using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
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
    TKey> : IBaseService<TContext, TEntity, TCreateRequest, TUpdateRequest, TViewModel, TKey>
    where TEntity : BaseEntity<TKey>, new()
    where TCreateRequest : BaseCreateRequest, new()
    where TUpdateRequest : BaseUpdateRequest<TKey>, new()
    where TViewModel : BaseViewModel<TKey>, new()
    where TContext : DbContext
{
    protected readonly ILogger _logger;
    protected readonly IMapper _mapper;
    protected readonly IUnitOffWork<TContext> _unitOffWork;

    protected BaseService(IMapper mapper, IUnitOffWork<TContext> unitOffWork, ILogger logger)
    {
        _mapper = mapper;
        _unitOffWork = unitOffWork;
        _logger = logger;
    }

    /// <summary>
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    public async Task<int?> DeleteHardAsync(TKey id)
    {
        await _unitOffWork.Repository<TEntity, TKey>().DeleteHardAsync(id!);
        return await _unitOffWork.SaveChangesAsync();
    }

    /// <summary>
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    public async Task<int?> DeleteSoftAsync(TKey id)
    {
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
        return result;
    }

    public virtual async Task<TViewModel?> GetByIdAsync(TKey id)
    {
        var entity = await _unitOffWork.Repository<TEntity, TKey>().GetByIdNoTrackingAsync(id);
        var result = _mapper.Map<TViewModel>(entity);
        return result;
    }

    public virtual async Task<TViewModel?> UpdateAsync(TKey id, TUpdateRequest request)
    {
        if (id is null || !id.Equals(request.Id))
            throw new KeyNotFoundException();
        var entity = await _unitOffWork.Repository<TEntity, TKey>().GetByIdAsync(id);

        if (entity == null) throw new NullReferenceException();
        entity = _mapper.Map(request, entity);
        var effectedCount = await _unitOffWork.Repository<TEntity, TKey>().UpdateAsync(entity);
        if (effectedCount <= 0) throw new NullReferenceException();
        var result = _mapper.Map<TViewModel>(entity);
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
            var entityNew = new TEntity();
            _mapper.Map(request, entityNew);
            var effectedCount =
                await _unitOffWork.Repository<TEntity, TKey>().CreateAsync(entityNew);
            if (effectedCount <= 0) throw new NullReferenceException();
            var result = _mapper.Map<TViewModel>(entityNew);
            return result;
        });
    }

    /// <summary>
    /// </summary>
    /// <param name="request"></param>
    /// <returns></returns>
    /// <exception cref="NullReferenceException"></exception>
    public virtual async Task<IEnumerable<TViewModel>?> CreateAsync(
        IEnumerable<TCreateRequest> request)
    {
        return await _unitOffWork.DoWorkWithTransaction(async () =>
        {
            var entitiesNew = new List<TEntity>();
            _mapper.Map(request, entitiesNew);
            IEnumerable<TEntity> response = new List<TEntity>();
            var affectedCount =
                await _unitOffWork.Repository<TEntity, TKey>().CreateAsync(entitiesNew);
            if (affectedCount <= 0) throw new NullReferenceException();
            var result = _mapper.Map<IEnumerable<TViewModel>>(response);
            return result;
        });
    }
}