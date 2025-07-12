namespace PodcastMcpServer.Helpers;

public class PodcastDetails
{
    public string Title { get; set; } = null!;
    public IReadOnlyList<string> Tags { get; set; } = null!;
}
