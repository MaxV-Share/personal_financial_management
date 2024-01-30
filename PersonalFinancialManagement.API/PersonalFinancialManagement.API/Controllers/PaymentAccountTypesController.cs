using Microsoft.AspNetCore.Mvc;
using PersonalFinancialManagement.API.Controllers.Base;
using PersonalFinancialManagement.Common.Models;
using PersonalFinancialManagement.Common.Models.DTOs;
using PersonalFinancialManagement.Models.DbContexts;
using PersonalFinancialManagement.Models.Dtos.TransactionCategoryType;
using PersonalFinancialManagement.Models.Dtos.TransactionCategoryType.Requests;
using PersonalFinancialManagement.Models.Entities;
using PersonalFinancialManagement.Services.Interfaces;

namespace PersonalFinancialManagement.API.Controllers;

public class PaymentAccountTypesController(
    ILogger<PaymentAccountTypesController> logger,
    IPaymentAccountTypeService paymentAccountTypeService
)
    : CrudController<ApplicationDbContext, PaymentAccountType, PaymentAccountTypeCreateRequest,
        PaymentAccountTypeUpdateRequest, PaymentAccountTypeViewModel, Guid>(logger,
            paymentAccountTypeService)
{
    [ProducesResponseType(StatusCodes.Status200OK,
        Type = typeof(BasePaging<PaymentAccountTypeViewModel>))]
    public override async Task<ActionResult<IBasePaging<PaymentAccountTypeViewModel>>> GetPaging(
        FilterBodyRequest request)
    {
        var result = await paymentAccountTypeService.GetPagingAsync(request);
        return Ok(result);
    }
}