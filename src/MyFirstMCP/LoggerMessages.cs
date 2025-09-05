namespace MyFirstMCP;

using Microsoft.Extensions.Logging;

public static partial class LoggerMessages
{
    [LoggerMessage(LogLevel.Debug, Message = "{message}")]
    public static partial void DebugOutput(this ILogger logger, string message);
}
