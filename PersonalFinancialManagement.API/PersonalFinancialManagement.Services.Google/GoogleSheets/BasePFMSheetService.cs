﻿using Microsoft.Extensions.Logging;

namespace PersonalFinancialManagement.GoogleServices.GoogleSheets;

public class BasePFMSheetService
{
    protected const string SpreadsheetId = "1lKVpkoHSCvm7cIiSgJfSBtIEGpk125WvegEorq8jbqw";
    protected const string RangeMailId = "B2:B";
    protected readonly ILogger _logger;

    public BasePFMSheetService(ILogger logger)
    {
        _logger = logger;
    }
}