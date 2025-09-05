namespace MyFirstMCP;

using System.ComponentModel;
using System.Diagnostics;

using Microsoft.Extensions.Logging;

using ModelContextProtocol.Server;

public class EchoTool(ILogger<EchoTool> logger)
{
    [McpServerTool]
    [Description("Echoes the message back to the client.")]
    public string Echo(string message)
    {
        logger.DebugOutput($"Echoing message: {message}");
        return $"Hello from C#: {message}";
    }

    [McpServerTool]
    [Description("Echoes in reverse the message sent by the client.")]
    public string ReverseEcho(string message)
    {
        logger.DebugOutput($"Reverse echoing message: {message}");
        return new string([..message.Reverse(), 'a', 'b']);
    }

    [McpServerTool]
    [Description("Returns the Process ID (PID) of the running server application.")]
    public int GetProcessId()
    {
        return Environment.ProcessId;
    }
}
