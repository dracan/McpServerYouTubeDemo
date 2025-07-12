using System.ComponentModel;
using Microsoft.Extensions.AI;
using ModelContextProtocol.Server;

namespace PodcastMcpServer;

[McpServerPromptType]
public sealed class ExamplePrompts
{
    [McpServerPrompt, Description("Prompt to get a list of podcasts with a given tag")]
    public ChatMessage GetListOfPodcastEpisodesWithGivenTag([Description("Episode tag")] string tag) =>
        new(ChatRole.User, $"Get a list of Unhandled Exception podcast episodes that have the tag '{tag}'");
}
