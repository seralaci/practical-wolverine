# Ping-Pong with RabbitMQ

A classic "ping/pong" sample of sending messages between two Wolverine processes using Rabbit MQ.

It also includes an example of code generation during Docker image creation and integration with `Aspire`.

## Aspire

> This solution shows how to extend the .NET Aspire application model to enable waiting for dependencies (in our case to RabbitMQ) to be available before starting the application ().
> The idea for extending the .NET Aspire application model comes from David Fowler's [`WaitForDependenciesAspire` GitHub repository](https://github.com/davidfowl/WaitForDependenciesAspire).

> [!WARNING]
> Remember to ensure that Docker is started

* Run the application from Visual Studio / Rider:
- Open the `PingPongWithRabbitMq.sln` file
- Ensure that `AppHost.csproj` is your startup project
- Hit Ctrl-F5 to launch Aspire

* Or run the application from your terminal:
```powershell
dotnet run --project src/Aspire/AppHost/AppHost.csproj
```
then look for lines like this in the console output in order to find the URL to open the Aspire dashboard:
```sh
Login to the dashboard at: http://localhost:17287/login?t=uniquelogincodeforyou
```

## Solution Structure and Basic Functionality

The solution consists of two main services:

### [Pinger](src/Pinger)

- Sends [`Ping`](src/Pinger/Messages/Ping.cs) messages to the to the `pings` queue from the [`PingerService`](src/Pinger/BackgroundServices/PingerService.cs) in a continuous loop.
- Listens to the `pongs` queue and handles the [`Pong`](src/Pinger/Messages/Pong.cs) messages in the [`PongHandler`](src/Pinger/Handlers/PongHandler.cs).

### [Ponger](src/Ponger)

- Listens to the `pings` queue and handles the [`Ping`](src/Ponger/Messages/Ping.cs) messages in the [`PingHandler`](src/Ponger/Handlers/PingHandler.cs).
- Responds with [`Pong`](src/Ponger/Messages/Pong.cs) messages to the `pongs` queue.

## Code Generation

In the Docker files, we use the `codegen` option to write the source code ahead of time, and by running the solution through `docker-compose`, the type load mode will be static in Production mode.

## RabbitMQ

A message broker service using the `rabbitmq:3-management-alpine` image. It exposes ports `5672` and `15672` and uses default credentials (`guest`/`guest`).


