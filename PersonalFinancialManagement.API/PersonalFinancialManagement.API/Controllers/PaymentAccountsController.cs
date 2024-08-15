using Microsoft.AspNetCore.Mvc;
using PersonalFinancialManagement.API.Controllers.Base;
using PersonalFinancialManagement.Common.Models;
using PersonalFinancialManagement.Common.Models.DTOs;
using PersonalFinancialManagement.Models.DbContexts;
using PersonalFinancialManagement.Models.Dtos.PaymentAccounts;
using PersonalFinancialManagement.Models.Dtos.PaymentAccounts.Requests;
using PersonalFinancialManagement.Models.Entities;
using PersonalFinancialManagement.Services.Interfaces;

namespace PersonalFinancialManagement.API.Controllers;

public class PaymentAccountsController(
    ILogger<PaymentAccountsController> logger,
    IPaymentAccountService paymentAccountService
)
    : CrudController<ApplicationDbContext, PaymentAccount, PaymentAccountCreateRequest,
        PaymentAccountUpdateRequest, PaymentAccountViewModel, Guid>(logger,
            paymentAccountService)
{
    [ProducesResponseType(StatusCodes.Status200OK,
        Type = typeof(BasePaging<PaymentAccountViewModel>))]
    public override async Task<ActionResult<IBasePaging<PaymentAccountViewModel>>> GetPaging(
        FilterBodyRequest request)
    {
        var result = await paymentAccountService.GetPagingAsync(request);
        return Ok(result);
    }
}