using AutoMapper;
using Microsoft.EntityFrameworkCore;
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
    ILogger<TransactionService> logger
)
    : BaseService<
        ApplicationDbContext,
        RawTransaction,
        RawTransactionCreateRequest,
        RawTransactionUpdateRequest,
        RawTransactionViewModel,
        Guid
    >(mapper, unitOffWork, logger),
        IRawTransactionService
{
    public async Task<List<string>> GetMailIdsAsync(string walletId)
    {
        return await _unitOffWork
            .Repository<RawTransaction, Guid>()
            .GetNoTrackingEntities()
            .Where(e => !string.IsNullOrEmpty(e.WalletId) && e.WalletId.Contains(walletId))
            .Select(e => e.WalletId ?? string.Empty)
            .ToListAsync();
    }

    public async Task<DateTime> GetLastSyncByWalletAsync(string walletId)
    {
        return await _unitOffWork
            .Repository<RawTransaction, Guid>()
            .GetNoTrackingEntities()
            .Where(e => !string.IsNullOrEmpty(e.WalletId) && e.WalletId.Contains(walletId))
            .Select(e => e.TransactionDate)
            .OrderByDescending(e => e)
            .FirstOrDefaultAsync();
    }
}
