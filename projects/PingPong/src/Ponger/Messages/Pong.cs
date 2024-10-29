using MemoryPack;
using Wolverine.Attributes;

namespace Ponger.Messages;

// The MemoryPackable attribute is used to indicate that this record should be serialized and deserialized
// using MemoryPack and should be partial because of MemoryPack's code generator
[MemoryPackable]
// We explicitly control the message type (by overriding with MessageIdentity attribute),
// because we aren't sharing the DTO types for learning purposes
[MessageIdentity("Pong")]
// Message type must be public
public partial record Pong(int Number);
