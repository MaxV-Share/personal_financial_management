
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
using PersonalFinancialManagement.Common.Models;
using PersonalFinancialManagement.Common.Models.DTOs;
using PersonalFinancialManagement.Common.Extensions;
using PersonalFinancialManagement.EFCore;
using PersonalFinancialManagement.Models.DbContexts;
using PersonalFinancialManagement.Models.Dtos.Currencies.Requests;
using PersonalFinancialManagement.Models.Dtos.Currencies;
using PersonalFinancialManagement.Models.Dtos.PaymentAccounts.Requests;
using PersonalFinancialManagement.Models.Dtos.PaymentAccounts;

namespace PersonalFinancialManagement.Services
{
    public class PaymentAccountService : BaseService<ApplicationDbContext, PaymentAccount, PaymentAccountCreateRequest, PaymentAccountUpdateRequest, PaymentAccountViewModel, Guid>, IPaymentAccountService
    {

        public PaymentAccountService(IMapper mapper, IUnitOffWork<ApplicationDbContext> unitOffWork, ILogger<PaymentAccountService> logger) : base(mapper, unitOffWork, logger)
        {
        }
        public override async Task<PaymentAccountViewModel?> CreateAsync(PaymentAccountCreateRequest request)
        {
            if (request == null)
                return null;

            PaymentAccountViewModel? result = null;
            await _unitOffWork.DoWorkWithTransaction(async () =>
            {
                var entity = _mapper.Map<PaymentAccount>(request);

                var countAffect = await _unitOffWork.Repository<PaymentAccount, Guid>().CreateAsync(entity);
                if (countAffect == 0)
                {
                    result = null;
                }

                result = _mapper.Map<PaymentAccountViewModel>(entity);
            });

            return result;

        }
        public override async Task<PaymentAccountViewModel> GetByIdAsync(Guid id)
        {
            var PaymentAccount = await _unitOffWork.Repository<PaymentAccount, Guid>().GetByIdNoTrackingAsync(id);
            var result = _mapper.Map<PaymentAccountViewModel>(PaymentAccount);
            return result;
        }
        public async Task<IBasePaging<PaymentAccountViewModel>> GetPagingAsync(IFilterBodyRequest request)
        {
            var query = _mapper.ProjectTo<PaymentAccountViewModel>(_unitOffWork.Repository<PaymentAccount, Guid>().GetNoTrackingEntities());


            if (!request.SearchValue.IsNullOrEmpty())
            {
                query = query.Where(e => e.Name.Contains(request.SearchValue));
            }

            return await query.ToPagingAsync(request);
        }
    }
}
