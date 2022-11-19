using PersonalFinancialManagement.Common.Models.DTOs;
using PersonalFinancialManagement.Common.Models;
using PersonalFinancialManagement.Services.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PersonalFinancialManagement.Models.Entities;
using PersonalFinancialManagement.Models.DbContexts;
using PersonalFinancialManagement.Models.Dtos.Currencies;
using PersonalFinancialManagement.Models.Dtos.Currencies.Requests;
using PersonalFinancialManagement.Models.Dtos.TransactionCategoryType.Requests;
using PersonalFinancialManagement.Models.Dtos.TransactionCategoryType;

namespace PersonalFinancialManagement.Services.Interfaces
{
    public interface IPaymentAccountTypeService : IBaseService<ApplicationDbContext, PaymentAccountType, PaymentAccountTypeCreateRequest, PaymentAccountTypeUpdateRequest, PaymentAccountTypeViewModel, Guid>
    {
        Task<IBasePaging<PaymentAccountTypeViewModel>?> GetPagingAsync(IFilterBodyRequest request);
    }
}
