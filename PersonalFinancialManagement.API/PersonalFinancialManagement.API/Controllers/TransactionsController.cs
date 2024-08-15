using Microsoft.AspNetCore.Mvc;
using PersonalFinancialManagement.API.Controllers.Base;
using PersonalFinancialManagement.Common.Models;
using PersonalFinancialManagement.Common.Models.DTOs;
using PersonalFinancialManagement.Models.DbContexts;
using PersonalFinancialManagement.Models.Dtos.Transactions;
using PersonalFinancialManagement.Models.Dtos.Transactions.Requests;
using PersonalFinancialManagement.Models.Entities;
using PersonalFinancialManagement.Services.Interfaces;

namespace PersonalFinancialManagement.API.Controllers;

public class TransactionsController(
    ILogger<TransactionsController> logger,
    ITransactionService transactionService
)
    : CrudController<ApplicationDbContext, Transaction, TransactionCreateRequest,
        TransactionUpdateRequest, TransactionViewModel, Guid>(logger, transactionService)
{
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(BasePaging<TransactionViewModel>))]
    public override async Task<ActionResult<IBasePaging<TransactionViewModel>>> GetPaging(
        FilterBodyRequest request)
    {
        var result = await transactionService.GetPagingAsync(request);
        return Ok(result);
    }

    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(BasePaging<TransactionViewModel>))]
    public override async Task<ActionResult> Post(TransactionCreateRequest? request)
    {
        if (request?.FromPaymentAccountId == null)
            return BadRequest();
        var result = await transactionService.CreateAsync(request);

        if (null == result)
            return StatusCode(StatusCodes.Status500InternalServerError);
        return Ok(result);
    }
}