using Microsoft.AspNetCore.Mvc;
using PersonalFinancialManagement.API.Controllers.Base;
using PersonalFinancialManagement.GoogleServices.Interfaces;

namespace PersonalFinancialManagement.API.Controllers;

public class JobsController : ApiController
{
    private readonly IDemoService _demoService;

    public JobsController(ILogger<JobsController> logger, IDemoService demoService) : base(logger)
    {
        _demoService = demoService;
    }

    [HttpGet("read-gmail")]
    public async Task<IActionResult> ReadGmail()
    {
        await _demoService.Run();
        //BackgroundJob.Enqueue<GmailServices>(x => x.Main());
        return Ok();
    }
}