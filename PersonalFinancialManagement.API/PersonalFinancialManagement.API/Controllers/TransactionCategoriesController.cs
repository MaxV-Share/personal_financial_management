using Microsoft.AspNetCore.Mvc;
using PersonalFinancialManagement.API.Controllers.Base;
using PersonalFinancialManagement.Common.Models;
using PersonalFinancialManagement.Common.Models.DTOs;
using PersonalFinancialManagement.Models.DbContexts;
using PersonalFinancialManagement.Models.Dtos.TransactionCategories;
using PersonalFinancialManagement.Models.Dtos.TransactionCategories.Requests;
using PersonalFinancialManagement.Models.Entities;
using PersonalFinancialManagement.Services.Interfaces;

namespace PersonalFinancialManagement.API.Controllers;

public class TransactionCategoriesController : CrudController<ApplicationDbContext, TransactionCategory, TransactionCategoryCreateRequest, TransactionCategoryUpdateRequest, TransactionCategoryViewModel, Guid>
{
    private readonly ITransactionCategoryService _transactionCategoryService;

    public TransactionCategoriesController(ILogger<TransactionCategoriesController> logger, ITransactionCategoryService transactionCategoryService) : base(logger, transactionCategoryService)
    {
        _transactionCategoryService = transactionCategoryService;
    }

    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(BasePaging<TransactionCategoryViewModel>))]
    public override async Task<ActionResult<IBasePaging<TransactionCategoryViewModel>>> GetPaging(FilterBodyRequest request)
    {
        var result = await _transactionCategoryService.GetPagingAsync(request);
        return Ok(result);
    }
}