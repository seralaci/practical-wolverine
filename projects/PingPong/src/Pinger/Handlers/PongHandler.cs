using Microsoft.Extensions.Logging;
using Pinger.Messages;
using Spectre.Console;

namespace Pinger.Handlers;

// Message handlers must be public types with a public constructor.
// It is valid to use class instances with constructor arguments for handlers,
// but using a static method as message handler can be a small performance improvement by avoiding the need to create
// and garbage collect new objects at runtime.
public class PongHandler(ILogger<PongHandler> logger)
{
    // Handler methods must be public and the first argument must be the message type
    public void Handle(Pong pong)
    {
        logger.LogInformation("Received Pong #{Number}", pong.Number);

        AnsiConsole.MarkupLine($"[blue]Got pong #{pong.Number}[/]");
    }
}
