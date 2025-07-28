This is the code for the following YouTube video (hosted on Nick Chapsas's channel)...
https://www.youtube.com/watch?v=DpyjAKmNwpI

The video describes what the code does - this readme is just to highlight some prerequisites for running this demo yourself.

# Prerequisites

## dotnet 9 SDK

https://dotnet.microsoft.com/en-us/download/dotnet/9.0

## OpenAI key (only required for the client-side MCP consumer)

If you're trying to get the clientside (ExampleClientApplication) working, then you'll need an OpenAI key.

You can then set it via a dotnet user secret, with the following dotnet command...

```
cd ExampleClientApplication
dotnet user-secrets set OpenAiApiKey <my openai key>
```

# Troubleshooting

If you're having trouble getting this to work - ensure you've built the solution before trying to add it as an MCP Server in vscode. The command in the video (and above) to install the MCP Server in vscode has `--no-build`, so you need to ensure you've built it manually beforehand. Double-check that build output for any errors.

If running the MCP Server in GitHub Copilot doesn't work - open the vscode output window via "mcp.json -> More -> Show Output", and inspect the output for any warnings or errors.
