using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

using MyFirstMCP;

using Serilog;
using Serilog.Context;
using Serilog.Formatting.Compact;

HostApplicationBuilder builder = Host.CreateApplicationBuilder(args);

LoggerConfiguration loggerConfiguration = new LoggerConfiguration()
    .WriteTo.Console(new CompactJsonFormatter())
    .Enrich.FromLogContext()
    .Enrich.WithMachineName()
    .MinimumLevel.Information();

Log.Logger = loggerConfiguration.CreateLogger();

builder.Services
    .AddMcpServer()
    .WithStdioServerTransport()
    .WithTools<EchoTool>();

IHost app = builder.Build();

await app.RunAsync();

internal static partial class Program;
