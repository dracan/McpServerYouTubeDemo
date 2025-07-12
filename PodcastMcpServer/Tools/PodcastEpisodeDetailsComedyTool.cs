using System.ComponentModel;
using Microsoft.Extensions.AI;
using ModelContextProtocol.Server;
using PodcastMcpServer.Helpers;

namespace PodcastMcpServer.Tools;

[McpServerToolType]
public static class PodcastEpisodeDetailsComedyTool
{
    [McpServerTool(Name = "GetComedyPodcastEpisodeDetails")]
    [Description("Gets a list of of Unhandled Exception podcast episodes, where the details have a comedy slant. Optionally takes in one or more tags.")]
    public static async Task<List<PodcastDetails>> GetComedyPodcastEpisodeDetails(
        IMcpServer thisServer,
        List<string>? tags,
        CancellationToken cancellationToken)
    {
        var podcastEpisodes = FrontMatterLoader.Load(@"C:\Code\UnhandledExceptionPodcastWebsite\content\posts\");

        var filteredEpisodes = podcastEpisodes.FilterByTags(tags);

        foreach (var filteredEpisode in filteredEpisodes)
        {
            var llmResponse = $"{await thisServer.AsSamplingChatClient().GetResponseAsync((ChatMessage[])
            [
                new(ChatRole.User,
                    $"Change this podcast title to be funny: \"{filteredEpisode.Title}\"." +
                    $"Do not include any other text in the response - just the title itself."),
            ], cancellationToken: cancellationToken)}";

            filteredEpisode.Title = llmResponse;
        }

        return filteredEpisodes;
    }
}
