using Pinger.Messages;
using Spectre.Console;

namespace Pinger.Handlers;

// Message handlers must be public types with a public constructor.
public static class PongHandler
{
    // Handler methods must be public and the first argument must be the message type
    // // Using a static method as your message handler can be a small performance improvement
    // // by avoiding the need to create and garbage collect new objects at runtime.
    public static void Handle(Pong pong)
    {
        AnsiConsole.MarkupLine($"[blue]Got pong #{pong.Number}[/]");
    }
}
