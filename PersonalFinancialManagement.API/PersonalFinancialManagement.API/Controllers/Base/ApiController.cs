using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace PersonalFinancialManagement.API.Controllers.Base
{
    [Route("api/[controller]")]
    [ApiController]
    public abstract class ApiController : ControllerBase
    {
        public readonly ILogger _logger;
        public ApiController(ILogger logger)
        {
            _logger = logger;
        }
    }
}
