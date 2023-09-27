using Swashbuckle.AspNetCore.SwaggerUI;

namespace SchoolApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class LogController : ControllerBase
{
    private readonly LogRepo logRepo;

    public LogController(LogRepo logRepo)
    {
        this.logRepo = logRepo;
    }

    [HttpGet("{subscriberName}/{LogType}")]
    public async Task<ActionResult<List<List<LogContent>>>> Get
        ([FromRoute]string subscriberName,[FromRoute]string logType)
    {
        List<List<LogContent>> AllLogs = await logRepo.GetAllLogsAsync(subscriberName,logType);
        return AllLogs;
    }

    [HttpPost("{subscriberName}/{LogType}")]
    public async Task<ActionResult<LogContent>> Post
        (   
            [FromRoute]string subscriberName,
            [FromRoute] string logType,
            [FromBody] string message
        )
    {
        switch (logType)
        {
            case "Information":
                logRepo.Information(subscriberName,message);
                break;
            case "Debug":
                logRepo.Debug(subscriberName,message);
                break;
            case "Error":
                logRepo.Error(subscriberName,message);
                break;
            case "Critical":
                logRepo.Critical(subscriberName,message);
                break;
            default:
                return BadRequest();
        }
        return CreatedAtAction(nameof(Post), message);

    }
}