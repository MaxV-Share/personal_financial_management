using Microsoft.AspNetCore.Mvc;
using PersonalFinancialManagement.API.Controllers.Base;
using PersonalFinancialManagement.Common.Models;
using PersonalFinancialManagement.Models.DbContexts;
using PersonalFinancialManagement.Models.Entities;
using PersonalFinancialManagement.Services;
using PersonalFinancialManagement.Services.Interfaces;
using PersonalFinancialManagement.Common.Models.DTOs;
using PersonalFinancialManagement.Models.Dtos.Currencies.Requests;
using PersonalFinancialManagement.Models.Dtos.Currencies;

namespace PersonalFinancialManagement.API.Controllers
{ 
    [ApiController]
    [Route("[controller]")]
    public class CurrenciesController : CrudController<ApplicationDbContext, Currency, CurrencyCreateRequest, CurrencyUpdateRequest, CurrencyViewModel, Guid>
    {
        private readonly ICurrencyService _currencyTypeService;

        public CurrenciesController(ILogger<CurrenciesController> logger, ICurrencyService currencyTypeService) : base(logger, currencyTypeService)
        {
            _currencyTypeService = currencyTypeService;
        }

        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(BasePaging<CurrencyViewModel>))]
        public override async Task<ActionResult<IBasePaging<CurrencyViewModel>>> GetPaging(FilterBodyRequest request)
        {
            var result = await _currencyTypeService.GetPagingAsync(request);
            return Ok(result);
        }
    }
}