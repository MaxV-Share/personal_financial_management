using Microsoft.AspNetCore.Mvc;
using PersonalFinancialManagement.API.Controllers.Base;
using PersonalFinancialManagement.Common.Models;
using PersonalFinancialManagement.Models.DbContexts;
using PersonalFinancialManagement.Models.Entities;
using PersonalFinancialManagement.Services.Interfaces;
using PersonalFinancialManagement.Common.Models.DTOs;
using PersonalFinancialManagement.Models.Dtos.PaymentAccounts;
using PersonalFinancialManagement.Models.Dtos.PaymentAccounts.Requests;

namespace PersonalFinancialManagement.API.Controllers
{
    public class PaymentAccountsController : CrudController<ApplicationDbContext, PaymentAccount, PaymentAccountCreateRequest, PaymentAccountUpdateRequest, PaymentAccountViewModel, Guid>
    {
        private readonly IPaymentAccountService _paymentAccountService;

        public PaymentAccountsController(ILogger<PaymentAccountsController> logger, IPaymentAccountService paymentAccountService) : base(logger, paymentAccountService)
        {
            _paymentAccountService = paymentAccountService;
        }

        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(BasePaging<PaymentAccountViewModel>))]
        public override async Task<ActionResult<IBasePaging<PaymentAccountViewModel>>> GetPaging(FilterBodyRequest request)
        {
            var result = await _paymentAccountService.GetPagingAsync(request);
            return Ok(result);
        }
    }
}