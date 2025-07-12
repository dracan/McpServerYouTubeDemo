using ExampleClientApplication;
using Microsoft.Extensions.AI;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

var builder = Host.CreateApplicationBuilder(args);
builder.Services.AddSingleton<App>();

var openAiApiKey = builder.Configuration.GetValue<string>("OpenAiApiKey");

builder.Services.AddChatClient(_ =>
        new OpenAI.Chat.ChatClient("gpt-4o-mini", openAiApiKey).AsIChatClient())
    .UseFunctionInvocation(configure: x =>
    {
        x.IncludeDetailedErrors = true;
    });

var host = builder.Build();

var app = host.Services.GetRequiredService<App>();

await app.ExecuteAsync();
