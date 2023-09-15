using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using SchoolApi.Repos;
using SchoolWebsite.shared;
using System.Collections.Generic;
using System.Net;

namespace SchoolApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class LogController:ControllerBase
{
    public LogRepo logRepo;

    public LogController(LogRepo logRepo)
    {
        this.logRepo = logRepo;
    }

    [HttpGet]
    public async Task<ActionResult<List<List<LogContent>>>> Get(LogType logType)
    {
        List<List<LogContent>> AllLogs = await logRepo.GetAllLogsAsync(logType);
        return AllLogs;
    }
   
    [HttpPost]
    public async Task<ActionResult<LogContent>> Post([FromRoute] LogType logType, string message)
    {
        switch (logType)
        {
            case LogType.Information:
                logRepo.Information(message);
                break;
            case LogType.Debug:
                logRepo.Debug(message);
                break;
            case LogType.Error:
                logRepo.Error(message);
                break;
            case LogType.Critical:
                logRepo.Critical(message);
                break;
            default:
                return BadRequest();
        }
        return CreatedAtAction(nameof(Post), message);

    }
}