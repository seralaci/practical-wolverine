using Ponger.Messages;
using Spectre.Console;
using Wolverine;

namespace Ponger.Handlers;

// Message handlers must be public types with a public constructor.
public static class PingHandler
{
    // Handler methods must be public and the first argument must be the message type
    // Using a static method as your message handler can be a small performance improvement
    // by avoiding the need to create and garbage collect new objects at runtime.
    // Using cascading messages (see Pong) allow us to automatically send out objects returned from the handler methods
    // without having to use IMessageContext.
    public static Pong Handle(Ping ping)
    {
        AnsiConsole.MarkupLine($"[blue]Got ping #{ping.Number}[/]");

        var response = ping.ToPong();

        return response;
    }
}
