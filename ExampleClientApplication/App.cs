using ModelContextProtocol.Client;
using ModelContextProtocol.Protocol;
using Microsoft.Extensions.AI;
using Dumpify;

namespace ExampleClientApplication;

public class App(IChatClient chatClient)
{
    public async Task ExecuteAsync()
    {
        // Create an instance of IMcpClient. This is using StdioClientTransport, so will also run the
        // server locally using the specified command.

        var mcpClient = await CreateMcpClient();

        // Direct call to a tool on the MCP Server. Useful for development, but not how you'd
        // do it in proper code - as it's the role of the LLM to decide what tools to call to
        // fulfil the requirements of a given prompt.

        await ExampleOfDirectCall(mcpClient);

        // Example of an LLM deciding what tools to call (which is the main usecase for MCP Servers)

        await ExampleUsingAPrompt(mcpClient);

        // Example where the MCP Server can leverage the client's LLM

        // await ExampleUsingAPromptWithSampling(mcpClient);
    }

    private async Task<IMcpClient> CreateMcpClient() =>
        await McpClientFactory.CreateAsync(new StdioClientTransport(new StdioClientTransportOptions
        {
            Name = "PodcastMcpServer",
            Command = "dotnet",
            Arguments = ["run", "--project", "C:/Code/YouTubeVideos/McpServerYouTubeDemo/PodcastMcpServer", "--no-build"],
        }), new McpClientOptions
        {
            Capabilities = new ClientCapabilities
            {
                Sampling = new SamplingCapability
                {
                    SamplingHandler = chatClient.CreateSamplingHandler(),
                }
            }
        });

    private static async Task ExampleOfDirectCall(IMcpClient mcpClient)
    {
        var result = await mcpClient.CallToolAsync("GetPodcastEpisodeDetails",
            new Dictionary<string, object?>
            {
                { "tags", new[] { "mediatr" } }
            });

        result.Dump("ExampleOfDirectCall");
    }

    private async Task ExampleUsingAPrompt(IMcpClient mcpClient)
    {
        const string prompt = "Get a list of Unhandled Exception Podcast episodes with tag 'aspire'";

        var tools = await mcpClient.ListToolsAsync();

        var response = await chatClient.GetResponseAsync(prompt, new() { Tools = [.. tools] });

        response.Text.Dump("ExampleUsingAPrompt");
    }

    private async Task ExampleUsingAPromptWithSampling(IMcpClient mcpClient)
    {
        const string prompt = "Get a list of Unhandled Exception Podcast episodes with tag 'messaging' that have a comedy slant in the name.";

        var tools = await mcpClient.ListToolsAsync();

        var response = await chatClient.GetResponseAsync(prompt, new() { Tools = [.. tools] });

        response.Text.Dump("ExampleUsingAPromptWithSampling");
    }
}
