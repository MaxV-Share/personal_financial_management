using Hangfire;
using Microsoft.AspNetCore.Mvc;
using PersonalFinancialManagement.API.Controllers.Base;
using PersonalFinancialManagement.Services.Mails;

namespace PersonalFinancialManagement.API.Controllers
{
    public class JobsController : ApiController
    {
        public JobsController(ILogger<JobsController> logger) : base(logger)
        {
        }

        [HttpGet("read-gmail")]
        public Task<IActionResult> ReadGmail()
        {
            BackgroundJob.Enqueue<GmailServices>(x => x.Main());
            return Task.FromResult<IActionResult>(Ok());
        }
    }
}
