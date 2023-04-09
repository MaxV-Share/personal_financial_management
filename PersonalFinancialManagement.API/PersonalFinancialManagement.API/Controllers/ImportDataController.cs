using Microsoft.AspNetCore.Mvc;
using PersonalFinancialManagement.API.Controllers.Base;
using PersonalFinancialManagement.Services.Excels;

namespace PersonalFinancialManagement.API.Controllers
{
    public class ImportDataController : ApiController
    {
        private readonly IExample _example;
        public ImportDataController(ILogger<ImportDataController> logger, IExample example) : base(logger)
        {
            _example = example;
        }

        [HttpDelete("test")]
        public async Task Test()
        {
            await _example.ReadXLS("");
        }
    }
}
