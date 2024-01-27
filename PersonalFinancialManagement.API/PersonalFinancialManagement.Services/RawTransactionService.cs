using AutoMapper;
using Microsoft.Extensions.Logging;
using PersonalFinancialManagement.Models.DbContexts;
using PersonalFinancialManagement.Models.Dtos.Google;
using PersonalFinancialManagement.Models.Entities;
using PersonalFinancialManagement.Repositories.UnitOffWorks;
using PersonalFinancialManagement.Services.Base;
using PersonalFinancialManagement.Services.Interfaces;

namespace PersonalFinancialManagement.Services;

public class RawTransactionService(
    IMapper mapper,
    IUnitOffWork<ApplicationDbContext> unitOffWork,
    ILogger<TransactionService> logger)
    :
        BaseService<ApplicationDbContext, RawTransaction, RawTransactionCreateRequest,
            RawTransactionUpdateRequest, RawTransactionViewModel,
            Guid>(mapper, unitOffWork, logger),
        IRawTransactionService
{
}