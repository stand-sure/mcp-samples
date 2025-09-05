using MyFirstMCP;
using Serilog;
using Serilog.Events;
using Serilog.Formatting.Compact;

var builder = WebApplication.CreateBuilder(args);

builder.Host.UseSerilog((context, provider, config) =>
{
    config
        .WriteTo.Console(
            new CompactJsonFormatter(),
            standardErrorFromLevel: LogEventLevel.Verbose)
        .Enrich.FromLogContext()
        .Enrich.WithMachineName()
        .MinimumLevel.Debug();
});

// Configure the MCP Server with HTTP transport
builder.Services
    .AddMcpServer()
    .WithHttpTransport()
    .WithTools<EchoTool>();

var app = builder.Build();

// Map the required MCP endpoints
app.MapMcp();

// This will start the web server
app.Run();

internal static partial class Program;
