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

    [HttpGet("{logType}")]
    public async Task<ActionResult<List<List<LogContent>>>> Get(string logType)
    {
        List<List<LogContent>> AllLogs = await logRepo.GetAllLogsAsync(logType);
        return AllLogs;
    }
   
    [HttpPost("{logType}")]
    public async Task<ActionResult<LogContent>> Post([FromRoute] string logType,[FromBody] string message)
    {
        switch (logType)
        {
            case "Information":
                logRepo.Information(message);
                break;
            case "Debug":
                logRepo.Debug(message);
                break;
            case "Error":
                logRepo.Error(message);
                break;
            case "Critical":
                logRepo.Critical(message);
                break;
            default:
                return BadRequest();
        }
        return CreatedAtAction(nameof(Post), message);

    }
}