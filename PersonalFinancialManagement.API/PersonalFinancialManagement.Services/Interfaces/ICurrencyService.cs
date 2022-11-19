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

namespace PersonalFinancialManagement.Services.Interfaces
{
    public interface ICurrencyService : IBaseService<ApplicationDbContext, Currency, CurrencyCreateRequest, CurrencyUpdateRequest, CurrencyViewModel, Guid>
    {
        Task<IBasePaging<CurrencyViewModel>?> GetPagingAsync(IFilterBodyRequest request);
    }
}
