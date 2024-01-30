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
    public async Task<List<string>> GetMailIdsAsync(string walletType)
    {
        return await _unitOffWork
            .Repository<RawTransaction, Guid>()
            .GetNoTrackingEntities()
            .Where(e =>
                !string.IsNullOrEmpty(e.WalletId) &&
                !string.IsNullOrEmpty(e.WalletType) &&
                e.WalletType.Contains(walletType))
            .Select(e => e.MailId ?? string.Empty)
            .ToListAsync();
    }

    public async Task<DateTime?> GetLastSyncByWalletAsync(string walletType)
    {
        return await _unitOffWork
            .Repository<RawTransaction, Guid>()
            .GetNoTrackingEntities()
            .Where(e =>
                !string.IsNullOrEmpty(e.WalletId) &&
                !string.IsNullOrEmpty(e.WalletType) &&
                e.WalletType.Contains(walletType))
            .Select(e => e.TransactionDate)
            .OrderByDescending(e => e)
            .FirstOrDefaultAsync();
    }
}