using System.ComponentModel;
using ModelContextProtocol.Server;

namespace PodcastMcpServer;

[McpServerResourceType]
public static class Resources
{
    [McpServerResource(MimeType = "text/plain"), Description("Returns the Podcast website URI")]
    public static string GetPodcastWebsiteUri() => "https://unhandledexceptionpodcast.com/";
}
