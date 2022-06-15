using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using System.Linq.Expressions;
using PersonalFinancialManagement.Services.Base;
using PersonalFinancialManagement.Repositories.UnitOffWorks;
using PersonalFinancialManagement.Common.Models.DTOs;
using PersonalFinancialManagement.Common.Models;

namespace PersonalFinancialManagement.Services.Base
{
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    /// <typeparam name="TCreateRequest"></typeparam>
    /// <typeparam name="TUpdateRequest"></typeparam>
    /// <typeparam name="TViewModel"></typeparam>
    /// <typeparam name="TKey"></typeparam>
    public abstract class BaseService<TEntity, TCreateRequest, TUpdateRequest, TViewModel, TKey> : IBaseService<TEntity, TCreateRequest, TUpdateRequest, TViewModel, TKey>
        where TEntity : BaseEntity<TKey>, new()
        where TCreateRequest : BaseCreateRequest, new()
        where TUpdateRequest : BaseUpdateRequest<TKey>, new()
        where TViewModel : BaseViewModel<TKey>, new()
    {
        protected readonly IMapper _mapper;
        protected readonly IUnitOffWork _unitOffWork;
        protected readonly ILogger _logger;
        protected BaseService(IMapper mapper, IUnitOffWork unitOffWork, ILogger logger)
        {
            _mapper = mapper;
            _unitOffWork = unitOffWork;
            _logger = logger;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<int> DeleteHardAsync(TKey id)
        {
            await _unitOffWork.Repository<TEntity, TKey>().DeleteHardAsync(id);
            return await _unitOffWork.SaveChangesAsync();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<int> DeleteSoftAsync(TKey id)
        {
            return await _unitOffWork.Repository<TEntity, TKey>().DeleteSoftAsync(id);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public virtual async Task<IEnumerable<TViewModel>> GetAllDTOAsync()
        {
            await Task.Delay(3000);
            var query = _unitOffWork.Repository<TEntity, TKey>();
            var result = await _mapper.ProjectTo<TViewModel>(query.GetNoTrackingEntities()).ToListAsync();
            return result;
        }
        public virtual async Task<TViewModel> GetByIdAsync(TKey id)
        {
            var entity = await _unitOffWork.Repository<TEntity, TKey>().GetByIdNoTrackingAsync(id);
            var result = _mapper.Map<TViewModel>(entity);
            return result;
        }
        public virtual async Task<int> UpdateAsync(TKey id, TUpdateRequest request)
        {
            if (!id.Equals(request.Id))
                throw new KeyNotFoundException();
            var entity = await _unitOffWork.Repository<TEntity, TKey>().GetByIdAsync(id);

            if (entity == null)
            {
                throw new NullReferenceException();
            }
            entity = _mapper.Map(request, entity);
            var result = await _unitOffWork.Repository<TEntity, TKey>().UpdateAsync(entity);
            return result;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        /// <exception cref="NullReferenceException"></exception>
        public virtual async Task<TViewModel> CreateAsync(TCreateRequest request) => await _unitOffWork.DoWorkWithTransaction(async () =>
        {
            var entityNew = new TEntity();
            _mapper.Map(request, entityNew);
            await _unitOffWork.Repository<TEntity, TKey>().CreateAsync(entityNew);
            var effectedCount = await _unitOffWork.SaveChangesAsync();
            if (effectedCount <= 0)
            {
                throw new NullReferenceException();
            }
            var result = _mapper.Map<TViewModel>(entityNew);
            return result;
        });

        /// <summary>
        /// 
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        /// <exception cref="NullReferenceException"></exception>
        public virtual async Task<IEnumerable<TViewModel>> CreateAsync(IEnumerable<TCreateRequest> request) => await _unitOffWork.DoWorkWithTransaction(async () =>
        {
            var entitiesNew = new List<TEntity>();
            _mapper.Map(request, entitiesNew);
            IEnumerable<TEntity> response = new List<TEntity>();
            var affectedCount = await _unitOffWork.Repository<TEntity, TKey>().CreateAsync(entitiesNew);
            if (affectedCount <= 0)
            {
                throw new NullReferenceException();
            }
            var result = _mapper.Map<IEnumerable<TViewModel>>(response);
            return result;
        });
    }
}
