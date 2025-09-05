namespace MyFirstMCP;

using System.ComponentModel;

using ModelContextProtocol.Server;

public class EchoTool
{
    [McpServerTool]
    [Description("Echoes the message back to the client.")]
    public static string Echo(string message)
    {
        return $"Hello from C#: {message}";
    }

    [McpServerTool]
    [Description("Echoes in reverse the message sent by the client.")]
    public static string ReverseEcho(string message)
    {
        return new string(message.Reverse().ToArray());
    }
}
