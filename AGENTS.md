# AI Agent for McpSamples

This document provides an overview of the AI agent built for the `McpSamples` project. The agent exposes a set of tools for performing simple string manipulations, serving as a foundational example of how to use the Model Context Protocol (MCP) in a .NET application.

## Available Tools

The following tools are available through this agent:

-   **`Echo(string message)`**
    -   **Description**: Echoes the provided message back to the client, prefixed with "Hello from C#".
    -   **Parameters**:
        -   `message` (string): The text to be echoed.

-   **`ReverseEcho(string message)`**
    -   **Description**: Takes a string message and returns it in reverse order.
    -   **Parameters**:
        -   `message` (string): The text to be reversed.

## How It Works

The agent is a .NET console application built on `Microsoft.Extensions.Hosting`. It uses the `ModelContextProtocol` library to create an MCP server that communicates with the AI Assistant over standard I/O (`stdio`).

### Tool Creation

Tools are defined in a C# class (e.g., `EchoTool`). Methods intended to be used by the agent are decorated with two key attributes:

1.  **`[McpServerTool]`**: Marks the method as a tool to be exposed by the MCP server.
2.  **`[Description]`**: Provides a natural language description that the AI uses to understand the tool's purpose and functionality.

If a tool requires dependencies, such as `ILogger`, it can be defined as an instance class and receive those dependencies via constructor injection.

### Tool Registration

In `Program.cs`, the tools are registered with the MCP server and the application's dependency injection container:

```c#
// 1. Register the tool class with the MCP server 

builder.Services
    .AddMcpServer()
    .WithStdioServerTransport()
    .WithTools<EchoTool>()
    .WithTools<YOUR_TOOL>;  // <-- Register tool here

// 2. Register the tool for dependency injection builder.Services.AddSingleton();
```

When a tool is invoked, the MCP server uses the DI container to create an instance of the tool class, automatically resolving any registered dependencies.

## Running the Server for AI Assistant Integration

There is a critical startup order that must be followed for the AI Assistant to connect to the running application process. Due to a race condition during IDE startup, the server must be running *before* the IDE attempts to connect.

**Correct Workflow:**

1.  **Start the MCP Server:** Run the `MyFirstMCP` application. Wait for the console to show that the Kestrel web server is running and listening on its port (e.g., `http://localhost:5272`).
2.  **Start Rider:** *After* the server is running, open the project in Rider.
3.  **Verify Connection:** The AI Assistant will now connect to the running process. You can verify this by asking it to run the `GetProcessId` tool and comparing the result to the PID of your running application.

If the connection is not working, the most likely cause is that the IDE was started before the server. The easiest way to fix this is to close the IDE, ensure the server is running, and then reopen the IDE. Starting a new chat with the AI Assistant may also re-trigger the connection attempt.

**Settings > Tools AI Assistant > Model Context Protocol (MCP)

```json
{
  "mcpServers": {
    "my-csharp-server": {
      "command": "npx",
      "args": [
        "-y",
        "mcp-remote@latest",
        "http://localhost:5272/sse"
      ]
    }
  }
}
```

## Usage Example

Here is an example of how to interact with the agent:

**User**:
> run reverse echo with message "hello world"

**AI Agent**:
> dlrow olleh
                       

## Coding Style & Naming
- Follow `.editorconfig`. Treat nullable warnings as actionable.
- C#: file-scoped namespaces; `PascalCase` for types/members; `camelCase` for locals/params.
- One public type per file; keep methods small and focused. Prefer explicit types for public APIs.
- XML doc comments should be on all public members.
- Names should not start with an underscore; instead, local members should be prefixed with `this.`. This makes it clear when arguments from a primary constructor are used, when instance values are used (which has a side effect risk), etc.
