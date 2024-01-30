using PersonalFinancialManagement.Models.DbContexts;
using PersonalFinancialManagement.Models.Dtos.Google;
using PersonalFinancialManagement.Models.Entities;
using PersonalFinancialManagement.Services.Base;

namespace PersonalFinancialManagement.Services.Interfaces;

public interface IRawTransactionService
    : IBaseService<
        ApplicationDbContext,
        RawTransaction,
        RawTransactionCreateRequest,
        RawTransactionUpdateRequest,
        RawTransactionViewModel,
        Guid
        >
{
    Task<List<string>> GetMailIdsAsync(string walletType);
    Task<DateTime?> GetLastSyncByWalletAsync(string walletType);
}