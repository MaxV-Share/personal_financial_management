using Microsoft.AspNetCore.Mvc;
using PersonalFinancialManagement.API.Controllers.Base;
using PersonalFinancialManagement.Common.Models;
using PersonalFinancialManagement.Common.Models.DTOs;
using PersonalFinancialManagement.Models.DbContexts;
using PersonalFinancialManagement.Models.Dtos.TransactionCategoryTypes;
using PersonalFinancialManagement.Models.Dtos.TransactionCategoryTypes.Requests;
using PersonalFinancialManagement.Models.Entities;
using PersonalFinancialManagement.Services.Interfaces;

namespace PersonalFinancialManagement.API.Controllers;

public class TransactionCategoryTypesController(
    ILogger<TransactionCategoryTypesController> logger,
    ITransactionCategoryTypeService transactionCategoryTypeService
)
    : CrudController<ApplicationDbContext, TransactionCategoryType,
        TransactionCategoryTypeCreateRequest, TransactionCategoryTypeUpdateRequest,
        TransactionCategoryTypeViewModel, Guid>(logger, transactionCategoryTypeService)
{
    [ProducesResponseType(StatusCodes.Status200OK,
        Type = typeof(BasePaging<TransactionCategoryTypeViewModel>))]
    public override async Task<ActionResult<IBasePaging<TransactionCategoryTypeViewModel>>>
        GetPaging(FilterBodyRequest request)
    {
        var result = await transactionCategoryTypeService.GetPagingAsync(request);
        return Ok(result);
    }
}