using PersonalFinancialManagement.Models.Dtos.Google;

namespace PersonalFinancialManagement.Services.Mails;

public abstract class CreditGmailService
{
    protected abstract RawTransactionViewModel? BuildTransactionRawDataModel(string ht
    );
}