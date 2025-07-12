using System.ComponentModel;
using ModelContextProtocol.Server;
using PodcastMcpServer.Helpers;

namespace PodcastMcpServer.Tools;

[McpServerToolType]
public static class PodcastEpisodeDetailsTool
{
    [McpServerTool(Name = "GetPodcastEpisodeDetails")]
    [Description("Gets a list of Unhandled Exception podcast episodes. Optionally takes in one or more tags.")]
    public static List<PodcastDetails> GetPodcastEpisodeDetails(
        List<string>? tags)
    {
        // Debugger.Launch(); // Very useful if you want to debug into your MCP Server code!
        // Log.Logger.Information("Called GetPodcastEpisodeDetails tool");

        var podcastEpisodes = FrontMatterLoader.Load(@"C:\Code\UnhandledExceptionPodcastWebsite\content\posts\");
        return podcastEpisodes.FilterByTags(tags);
    }
}
