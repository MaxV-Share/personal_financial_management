
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Net.Http;
using PersonalFinancialManagement.Services;
using PersonalFinancialManagement.Services.Base;
using PersonalFinancialManagement.Repositories.UnitOffWorks;
using PersonalFinancialManagement.Models.Entities;
using PersonalFinancialManagement.Services.Interfaces;
using PersonalFinancialManagement.Models.Dtos.TransactionCategoryType.Requests;
using PersonalFinancialManagement.Models.Dtos.TransactionCategoryType;
using PersonalFinancialManagement.Common.Models;
using PersonalFinancialManagement.Common.Models.DTOs;
using PersonalFinancialManagement.Common.Extensions;
using PersonalFinancialManagement.EFCore;
using PersonalFinancialManagement.Models.DbContexts;

namespace PersonalFinancialManagement.Services
{
    public class TransactionCategoryTypeService : BaseService<ApplicationDbContext, TransactionCategoryType, TransactionCategoryTypeCreateRequest, TransactionCategoryTypeUpdateRequest, TransactionCategoryTypeViewModel, Guid>, ITransactionCategoryTypeService
    {

        public TransactionCategoryTypeService(IMapper mapper, IUnitOffWork<ApplicationDbContext> unitOffWork, ILogger<TransactionCategoryTypeService> logger) : base(mapper, unitOffWork, logger)
        {
        }
        public override async Task<TransactionCategoryTypeViewModel?> CreateAsync(TransactionCategoryTypeCreateRequest request)
        {
            if (request == null)
                return null;

            TransactionCategoryTypeViewModel? result = null;
            await _unitOffWork.DoWorkWithTransaction(async () =>
            {
                var entity = _mapper.Map<TransactionCategoryType>(request);

                var countAffect = await _unitOffWork.Repository<TransactionCategoryType, Guid>().CreateAsync(entity);
                if (countAffect == 0)
                {
                    result = null;
                }

                result = _mapper.Map<TransactionCategoryTypeViewModel>(entity);
            });

            return result;

        }
        public override async Task<TransactionCategoryTypeViewModel> GetByIdAsync(Guid id)
        {
            var TransactionCategoryType = await _unitOffWork.Repository<TransactionCategoryType, Guid>().GetByIdNoTrackingAsync(id);
            var result = _mapper.Map<TransactionCategoryTypeViewModel>(TransactionCategoryType);
            return result;
        }
        public async Task<IBasePaging<TransactionCategoryTypeViewModel>> GetPagingAsync(IFilterBodyRequest request)
        {
            var query = _mapper.ProjectTo<TransactionCategoryTypeViewModel>(_unitOffWork.Repository<TransactionCategoryType, Guid>().GetNoTrackingEntities());


            if (!request.SearchValue.IsNullOrEmpty())
            {
                query = query.Where(e => e.Name.Contains(request.SearchValue));
            }

            return await query.ToPagingAsync(request);
        }
    }
}
