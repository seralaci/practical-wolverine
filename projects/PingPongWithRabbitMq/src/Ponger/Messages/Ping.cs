using Wolverine.Attributes;

namespace Ponger.Messages;

// We explicitly control the message type (by overriding with MessageIdentity attribute),
// because we aren't sharing the DTO types for learning purposes
[MessageIdentity("Ping")]
// Message type must be public
public record Ping(int Number);

public static class PingExtensions
{
    public static Pong ToPong(this Ping ping) => new(ping.Number);
}
