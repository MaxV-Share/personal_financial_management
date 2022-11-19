using Microsoft.AspNetCore.Mvc;
using PersonalFinancialManagement.API.Controllers.Base;
using PersonalFinancialManagement.Common.Models;
using PersonalFinancialManagement.Models.DbContexts;
using PersonalFinancialManagement.Models.Entities;
using PersonalFinancialManagement.Services;
using PersonalFinancialManagement.Services.Interfaces;
using PersonalFinancialManagement.Common.Models.DTOs;
using PersonalFinancialManagement.Models.Dtos.TransactionCategoryType.Requests;
using PersonalFinancialManagement.Models.Dtos.TransactionCategoryType;

namespace PersonalFinancialManagement.API.Controllers
{ 
    [ApiController]
    [Route("[controller]")]
    public class PaymentAccountTypesController : CrudController<ApplicationDbContext, PaymentAccountType, PaymentAccountTypeCreateRequest, PaymentAccountTypeUpdateRequest, PaymentAccountTypeViewModel, Guid>
    {
        private readonly IPaymentAccountTypeService _paymentAccountTypeService;

        public PaymentAccountTypesController(ILogger<PaymentAccountTypesController> logger, IPaymentAccountTypeService paymentAccountTypeService) : base(logger, paymentAccountTypeService)
        {
            _paymentAccountTypeService = paymentAccountTypeService;
        }

        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(BasePaging<PaymentAccountTypeViewModel>))]
        public override async Task<ActionResult<IBasePaging<PaymentAccountTypeViewModel>>> GetPaging(FilterBodyRequest request)
        {
            var result = await _paymentAccountTypeService.GetPagingAsync(request);
            return Ok(result);
        }
    }
}