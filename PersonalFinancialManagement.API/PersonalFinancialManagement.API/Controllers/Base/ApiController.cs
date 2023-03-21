using Microsoft.AspNetCore.Mvc;

namespace PersonalFinancialManagement.API.Controllers.Base
{
    [Route("api/[controller]")]
    [ApiController]
    public abstract class ApiController : ControllerBase
    {
        public readonly ILogger _logger;
        protected ApiController(ILogger logger)
        {
            _logger = logger;
        }
    }
}
