using Wolverine.Attributes;

namespace Pinger.Messages;

// We explicitly control the message type (by overriding with MessageIdentity attribute),
// because we aren't sharing the DTO types for learning purposes
[MessageIdentity("Ping")]
// Message type must be public
public record Ping(int Number);
