namespace PersonalFinancialManagement.Models.Dtos.RawTransactions;

public class GetOldDataInGoogleSheetResult(
    DateTime? lastUpdate,
    List<string>? mailIds
)
{
    public GetOldDataInGoogleSheetResult() : this(null, null)
    {
    }

    public DateTime? LastUpdate { get; set; } = lastUpdate;
    public List<string>? MailIds { get; set; } = mailIds;
}