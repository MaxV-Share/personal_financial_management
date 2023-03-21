
using AutoMapper;
using Microsoft.Extensions.Logging;
using PersonalFinancialManagement.Services.Base;
using PersonalFinancialManagement.Repositories.UnitOffWorks;
using PersonalFinancialManagement.Models.Entities;
using PersonalFinancialManagement.Services.Interfaces;
using PersonalFinancialManagement.Common.Models;
using PersonalFinancialManagement.Common.Models.DTOs;
using PersonalFinancialManagement.Common.Extensions;
using PersonalFinancialManagement.EFCore;
using PersonalFinancialManagement.Models.DbContexts;
using PersonalFinancialManagement.Models.Dtos.TransactionCategoryTypes;
using PersonalFinancialManagement.Models.Dtos.TransactionCategoryTypes.Requests;

namespace PersonalFinancialManagement.Services
{
    public class TransactionCategoryTypeService : BaseService<ApplicationDbContext, TransactionCategoryType, TransactionCategoryTypeCreateRequest, TransactionCategoryTypeUpdateRequest, TransactionCategoryTypeViewModel, Guid>, ITransactionCategoryTypeService
    {

        public TransactionCategoryTypeService(IMapper mapper, IUnitOffWork<ApplicationDbContext> unitOffWork, ILogger<TransactionCategoryTypeService> logger) : base(mapper, unitOffWork, logger)
        {
        }
        public override async Task<TransactionCategoryTypeViewModel?> CreateAsync(TransactionCategoryTypeCreateRequest? request)
        {
            if (request == null)
                return null;

            TransactionCategoryTypeViewModel? result = null;

            async void Action()
            {
                var entity = _mapper.Map<TransactionCategoryType>(request);

                var countAffect = await _unitOffWork.Repository<TransactionCategoryType, Guid>().CreateAsync(entity);
                if (countAffect == 0)
                {
                    result = null;
                }

                result = _mapper.Map<TransactionCategoryTypeViewModel>(entity);
            }

            await _unitOffWork.DoWorkWithTransaction(Action);

            return result;

        }
        public override async Task<TransactionCategoryTypeViewModel?> GetByIdAsync(Guid id)
        {
            var transactionCategoryType = await _unitOffWork.Repository<TransactionCategoryType, Guid>().GetByIdNoTrackingAsync(id);
            var result = _mapper.Map<TransactionCategoryTypeViewModel>(transactionCategoryType);
            return result;
        }
        public async Task<IBasePaging<TransactionCategoryTypeViewModel>?> GetPagingAsync(IFilterBodyRequest request)
        {
            var query = _mapper.ProjectTo<TransactionCategoryTypeViewModel>(_unitOffWork.Repository<TransactionCategoryType, Guid>().GetNoTrackingEntities());


            if (!request.SearchValue.IsNullOrEmpty())
            {
                query = query.Where(e => e.Name!.Contains(request.SearchValue ?? ""));
            }

            return await query.ToPagingAsync(request);
        }
    }
}
