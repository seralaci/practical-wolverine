# Ping-Pong with RabbitMQ

A classic "ping/pong" sample of sending messages between two Wolverine processes using Rabbit MQ.

## Solution Structure and Basic Functionality

The solution consists of three main services defined in the `docker-compose.yml` file:

### RabbitMQ

A message broker service using the `rabbitmq:3-management-alpine` image. It exposes ports `5672` and `15672` and uses default credentials (`guest`/`guest`).

### [Pinger](src/Pinger)

- Sends [`Ping`](src/Pinger/Messages/Ping.cs) messages to the to the `pings` queue from the [`PingerService`](src/Pinger/BackgroundServices/PingerService.cs) in a continuous loop.
- Listens to the `pongs` queue and handles the [`Pong`](src/Pinger/Messages/Pong.cs) messages in the [`PongHandler`](src/Pinger/Handlers/PongHandler.cs).

### [Ponger](src/Ponger)

- Listens to the `pings` queue and handles the [`Ping`](src/Ponger/Messages/Ping.cs) messages in the [`PingHandler`](src/Ponger/Handlers/PingHandler.cs).
- Responds with [`Pong`](src/Ponger/Messages/Pong.cs) messages to the `pongs` queue.

## Code Generation

In the Docker files, we use the `codegen` option to write the source code ahead of time, and by running the solution through `docker-compose`, the type load mode will be static in Production mode.


