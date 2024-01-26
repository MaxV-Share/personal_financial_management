namespace PersonalFinancialManagement.Common.Models.Configurations.Google;

public class GoogleCloudSetting
{
    public GoogleSheetSetting GoogleSheetSetting { get; set; } = new();
    public GmailAccountSetting GmailAccountSetting { get; set; } = new();
    public string ApplicationName { get; set; } = "";
}