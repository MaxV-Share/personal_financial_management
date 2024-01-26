using Hangfire;
using Microsoft.AspNetCore.Mvc;
using PersonalFinancialManagement.API.Controllers.Base;
using PersonalFinancialManagement.Services.Jobs;

namespace PersonalFinancialManagement.API.Controllers;

public class JobsController : ApiController
{
    private readonly VpBankCreditJob _vpBankCreditJob;

    public JobsController(ILogger<JobsController> logger, VpBankCreditJob vpBankCreditJob) :
        base(logger)
    {
        _vpBankCreditJob = vpBankCreditJob;
    }

    [HttpGet("recurring-job-vpBank-credit")]
    public IActionResult RecurringJobVpBankCredit()
    {
        //await _demoService.Run();
        RecurringJob.AddOrUpdate<VpBankCreditJob>("VpBank-Credit-Job", e => e.Process(),
            "*/10 * * * *");
        return Ok();
    }

    [HttpGet("run-background-job-vpBank-credit")]
    public IActionResult RunJobVpBankCredit()
    {
        BackgroundJob.Enqueue<VpBankCreditJob>(e => e.Process());
        return Ok();
    }

    [HttpGet("sync-vpBank-credit")]
    public async Task<IActionResult> RunVpBankCredit()
    {
        await _vpBankCreditJob.Process();
        return Ok();
    }
}