﻿using Oakton;
using Pinger.BackgroundServices;
using Pinger.Messages;
using Wolverine;
using Wolverine.RabbitMQ;

namespace Pinger;

public static class Program
{
    public static async Task<int> Main(string[] args)
    {
        return await Host
            .CreateDefaultBuilder(args)
            .UseWolverine(opts =>
            {
                // Listen for messages coming into the pongs queue
                opts.ListenToRabbitQueue("pongs")
                    .PreFetchCount(100) // Prefetch 100 messages at a time
                    .ListenerCount(5); // Use 5 listener endpoints

                // Publish messages to the pings queue
                opts.PublishMessage<Ping>()
                    .ToRabbitQueue("pings");

                // Configure Rabbit MQ connection to the connection string
                // named "rabbit" from IConfiguration. This is *a* way to use
                // Wolverine + Rabbit MQ using Aspire
                opts.UseRabbitMqUsingNamedConnection("rabbit")
                    // Directs Wolverine to build any declared queues, exchanges, or
                    // bindings with the Rabbit MQ broker as part of bootstrapping time
                    .AutoProvision();

                //  Registering the hosted service here to send ping messages on a continuous loop
                opts.Services.AddHostedService<PingerService>();

                // Override the application assembly to help Wolverine find its handlers
                opts.ApplicationAssembly = typeof(Program).Assembly;

                // Use "Auto" type load mode at development time, but "Static" any other time
                opts.OptimizeArtifactWorkflow();
            })
            .RunOaktonCommands(args);
    }
}