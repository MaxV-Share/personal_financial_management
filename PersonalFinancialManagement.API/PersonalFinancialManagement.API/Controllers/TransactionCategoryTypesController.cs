using Microsoft.AspNetCore.Mvc;
using PersonalFinancialManagement.API.Controllers.Base;
using PersonalFinancialManagement.Common.Models;
using PersonalFinancialManagement.Models.DbContexts;
using PersonalFinancialManagement.Models.Dtos.TransactionCategoryType.Requests;
using PersonalFinancialManagement.Models.Dtos.TransactionCategoryType;
using PersonalFinancialManagement.Models.Entities;
using PersonalFinancialManagement.Services;
using PersonalFinancialManagement.Services.Interfaces;
using PersonalFinancialManagement.Common.Models.DTOs;

namespace PersonalFinancialManagement.API.Controllers
{ 
    [ApiController]
    [Route("[controller]")]
    public class TransactionCategoryTypesController : CrudController<ApplicationDbContext, TransactionCategoryType, TransactionCategoryTypeCreateRequest, TransactionCategoryTypeUpdateRequest, TransactionCategoryTypeViewModel, Guid>
    {
        private readonly ITransactionCategoryTypeService _transactionCategoryTypeService;

        public TransactionCategoryTypesController(ILogger<TransactionCategoryTypesController> logger, ITransactionCategoryTypeService transactionCategoryTypeService) : base(logger, transactionCategoryTypeService)
        {
            _transactionCategoryTypeService = transactionCategoryTypeService;
        }

        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(BasePaging<TransactionCategoryTypeViewModel>))]
        public override async Task<ActionResult<IBasePaging<TransactionCategoryTypeViewModel>>> GetPaging(IFilterBodyRequest request)
        {
            var result = await _transactionCategoryTypeService.GetPagingAsync(request);
            return Ok(result);
        }
    }
}