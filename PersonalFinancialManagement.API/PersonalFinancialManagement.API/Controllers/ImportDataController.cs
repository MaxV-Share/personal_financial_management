using Microsoft.AspNetCore.Mvc;
using PersonalFinancialManagement.API.Controllers.Base;
using PersonalFinancialManagement.Services.Excels;

namespace PersonalFinancialManagement.API.Controllers;

public class ImportDataController(
    ILogger<ImportDataController> logger,
    IExample example
)
    : ApiController(logger)
{
    [HttpDelete("test")]
    public async Task Test()
    {
        await example.ReadXLS("");
    }
}