using System.Reflection.Metadata.Ecma335;

namespace SchoolWebsite.shared;

public class Subscriber
{
    public string? Name { get; set; }
    public List<LogContent> LogContents { get; set; }
}
