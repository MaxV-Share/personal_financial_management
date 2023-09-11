using Microsoft.AspNetCore.Mvc;
using PersonalFinancialManagement.API.Controllers.Base;
using PersonalFinancialManagement.Common.Models;
using PersonalFinancialManagement.Common.Models.DTOs;
using PersonalFinancialManagement.Models.DbContexts;
using PersonalFinancialManagement.Models.Dtos.TransactionCategories;
using PersonalFinancialManagement.Models.Dtos.TransactionCategories.Requests;
using PersonalFinancialManagement.Models.Dtos.TransactionCategoryTypes.Requests;
using PersonalFinancialManagement.Models.Dtos.TransactionCategoryTypes;
using PersonalFinancialManagement.Models.Entities;
using PersonalFinancialManagement.Services.Interfaces;
using PersonalFinancialManagement.Services;

namespace PersonalFinancialManagement.API.Controllers;

public class TransactionCategoriesController : CrudController<ApplicationDbContext, TransactionCategory, TransactionCategoryCreateRequest, TransactionCategoryUpdateRequest, TransactionCategoryViewModel, Guid>
{
    private readonly ITransactionCategoryService _transactionCategoryService;

    public TransactionCategoriesController(ILogger<TransactionCategoryTypesController> logger, ITransactionCategoryService transactionCategoryService) : base(logger, transactionCategoryService)
    {
        _transactionCategoryService = transactionCategoryService;
    }

    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(BasePaging<TransactionCategoryTypeViewModel>))]
    public override async Task<ActionResult<IBasePaging<TransactionCategoryViewModel>>> GetPaging(FilterBodyRequest request)
    {
        var result = await _transactionCategoryService.GetPagingAsync(request);
        return Ok(result);
    }
}