using Google.Apis.Sheets.v4.Data;

namespace PersonalFinancialManagement.GoogleServices;

public static class GoogleSheetHelper
{
    public static Sheet? GetSheetByName(this Spreadsheet spreadsheet, string name)
    {
        return spreadsheet.Sheets.SingleOrDefault(e => e.Properties.Title == name);
    }
}