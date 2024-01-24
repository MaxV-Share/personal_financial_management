using Hangfire;
using Microsoft.AspNetCore.Mvc;
using PersonalFinancialManagement.API.Controllers.Base;
using PersonalFinancialManagement.Services.Jobs;

namespace PersonalFinancialManagement.API.Controllers;

public class JobsController : ApiController
{
    public JobsController(ILogger<JobsController> logger) : base(logger)
    {
    }

    [HttpGet("recurring-job-vpBank-credit")]
    public Task<IActionResult> RecurringJobVpBankCredit()
    {
        //await _demoService.Run();
        RecurringJob.AddOrUpdate<VpBankCreditJob>("VpBank-Credit-Job", e => e.Process(),
            "*/10 * * * *");
        return Task.FromResult<IActionResult>(Ok());
    }

    [HttpGet("run-job-vpBank-credit")]
    public Task<IActionResult> RunJobVpBankCredit()
    {
        //await _demoService.Run();
        BackgroundJob.Enqueue<VpBankCreditJob>(e => e.Process());
        return Task.FromResult<IActionResult>(Ok());
    }
}