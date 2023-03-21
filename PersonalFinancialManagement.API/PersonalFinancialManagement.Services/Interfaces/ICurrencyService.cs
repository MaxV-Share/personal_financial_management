using PersonalFinancialManagement.Common.Models.DTOs;
using PersonalFinancialManagement.Common.Models;
using PersonalFinancialManagement.Services.Base;
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
