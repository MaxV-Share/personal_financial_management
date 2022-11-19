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
        private readonly ICurrencyService _transactionCategoryTypeService;

        public CurrenciesController(ILogger<CurrenciesController> logger, ICurrencyService transactionCategoryTypeService) : base(logger, transactionCategoryTypeService)
        {
            _transactionCategoryTypeService = transactionCategoryTypeService;
        }

        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(BasePaging<CurrencyViewModel>))]
        public override async Task<ActionResult<IBasePaging<CurrencyViewModel>>> GetPaging(FilterBodyRequest request)
        {
            var result = await _transactionCategoryTypeService.GetPagingAsync(request);
            return Ok(result);
        }
    }
}