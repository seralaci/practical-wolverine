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
    // (small disadvantage: in this case ILogger<Ping> should be used instead of ILogger<PingHandler>)
    public static ValueTask Handle(Ping ping, IMessageContext context)
    {
        AnsiConsole.MarkupLine($"[blue]Got ping #{ping.Number}[/]");

        var response = ping.ToPong();

        // RespondToSenderAsync can be used as well, it uses a dedicated response queue for the response
        // return context.RespondToSenderAsync(response);

        return context.SendAsync(response);
    }
}
