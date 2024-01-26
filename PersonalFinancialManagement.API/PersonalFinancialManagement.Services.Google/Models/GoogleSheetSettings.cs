namespace PersonalFinancialManagement.GoogleServices.Models;

/// <summary>
///     Settings for connect to google sheet object
/// </summary>
public class GoogleSheetSettings
{
    public string SheetName { get; set; } = "";

    public string SheetId { get; set; } = "";

    public string FCredentialFile { get; set; } = "";
}