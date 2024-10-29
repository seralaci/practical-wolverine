# Ping-Pong

A classic "ping/pong" sample of sending messages between two Wolverine processes using the TCP transport.

## Solution Structure and Basic Functionality

The solution consists of two main projects: `Pinger` and `Ponger`.

### [Pinger](src/Pinger)
- Sends [`Ping`](src/Pinger/Messages/Ping.cs) messages to the [`Ponger`](src/Ponger) service from the [`PingerService`](src/Pinger/BackgroundServices/PingerService.cs).
- Listens for [`Pong`](src/Pinger/Messages/Pong.cs) responses and handles them in the [`PongHandler`](src/Pinger/Handlers/PongHandler.cs).

### [Ponger](src/Ponger)
- Listens for [`Ping`](src/Ponger/Messages/Ping.cs) messages and handles them in the [`PingHandler`](src/Ponger/Handlers/PingHandler.cs).
- Responds with [`Pong`](src/Ponger/Messages/Pong.cs) messages.

### Key Components
- **Messages**: Define the structure of the messages exchanged between services. For learning purposes, they are not in a shared project, since in real life the actors of the distributed application are not necessarily part of the same solution.
- **Handlers**: Handle incoming messages and generate responses.
- **Serialization**: Uses MemoryPack for efficient serialization and deserialization of messages.
- **Transport**: Utilizes Wolverine's built-in TCP transport for communication between services.
