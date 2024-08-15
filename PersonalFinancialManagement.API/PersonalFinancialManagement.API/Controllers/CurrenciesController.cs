using Microsoft.AspNetCore.Mvc;
using PersonalFinancialManagement.API.Controllers.Base;
using PersonalFinancialManagement.Common.Models;
using PersonalFinancialManagement.Common.Models.DTOs;
using PersonalFinancialManagement.Models.DbContexts;
using PersonalFinancialManagement.Models.Dtos.Currencies;
using PersonalFinancialManagement.Models.Dtos.Currencies.Requests;
using PersonalFinancialManagement.Models.Entities;
using PersonalFinancialManagement.Services.Interfaces;

namespace PersonalFinancialManagement.API.Controllers;

public class CurrenciesController(
    ILogger<CurrenciesController> logger,
    ICurrencyService currencyTypeService
)
    : CrudController<ApplicationDbContext, Currency, CurrencyCreateRequest,
        CurrencyUpdateRequest, CurrencyViewModel, Guid>(logger, currencyTypeService)
{
    protected readonly ICurrencyService _currencyTypeService = currencyTypeService;

    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(BasePaging<CurrencyViewModel>))]
    public override async Task<ActionResult<IBasePaging<CurrencyViewModel>>> GetPaging(
        FilterBodyRequest request)
    {
        var result = await _currencyTypeService.GetPagingAsync(request);
        return Ok(result);
    }
}