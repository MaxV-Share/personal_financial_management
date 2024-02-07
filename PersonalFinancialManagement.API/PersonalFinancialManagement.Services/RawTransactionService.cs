using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using PersonalFinancialManagement.Common;
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
    public override Task<IEnumerable<RawTransactionViewModel>?> CreateAsync(
        List<RawTransactionCreateRequest> request)
    {
        var newMailIds = request.Select(e => e.MailId).ToList();
        var mailIdsExistInDb = _unitOffWork.Repository<RawTransaction, Guid>()
            .GetNoTrackingEntities()
            .Where(e => e.MailId != null && newMailIds.Contains(e.MailId)).Select(e => e.MailId)
            .ToList();
        var newRawTransaction = request.Where(e => !mailIdsExistInDb.Contains(e.MailId)).ToList();

        return base.CreateAsync(newRawTransaction);
    }

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

    public async Task<bool> UpdateGoogleDriveSyncedAsync(string mailId)
    {
        if (string.IsNullOrEmpty(mailId))
        {
            logger.LogDebug("mailId is null");
            return false;
        }

        var transaction = await _unitOffWork
            .Repository<RawTransaction, Guid>()
            .GetQueryableTable()
            .SingleOrDefaultAsync(e => e.MailId == mailId);
        if (transaction == null)
        {
            logger.LogDebug("transaction is null");
            return false;
        }

        transaction.GoogleDriveSynced = true;
        var countEffect = await _unitOffWork
            .Repository<RawTransaction, Guid>()
            .UpdateAsync(transaction);

        return countEffect > 0;
    }

    public async Task<bool> UpdateGoogleDriveSyncedAsync(string[] mailId)
    {
        if (!mailId.Any())
        {
            logger.LogDebug("mailId is empty");
            return false;
        }

        var transactions = await _unitOffWork
            .Repository<RawTransaction, Guid>()
            .GetQueryableTable()
            .Where(e => mailId.Contains(e.MailId))
            .ToListAsync();
        if (!transactions.Any())
        {
            logger.LogDebug("transactions is null");
            return false;
        }

        transactions.ForEach(e => e.GoogleDriveSynced = true);

        var countEffect = await _unitOffWork
            .Repository<RawTransaction, Guid>()
            .UpdateAsync(transactions);
        if (countEffect == 0)
            return false;
        if (transactions.Count == countEffect)
            return true;
        logger.LogDebug(
            $"Transaction update incorrect: {transactions.Where(e => e.GoogleDriveSynced != true).TryParseToString()}");
        return false;
    }
}