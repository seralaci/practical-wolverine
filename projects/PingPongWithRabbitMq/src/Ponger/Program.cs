using Oakton;
using Ponger.Messages;
using Wolverine;
using Wolverine.RabbitMQ;

namespace Ponger;

public static class Program
{
    public static async Task<int> Main(string[] args)
    {
        return await Host
            .CreateDefaultBuilder(args)
            .UseWolverine(opts =>
            {
                // Going to listen to a queue named "pings"
                opts.ListenToRabbitQueue("pings")
                    .PreFetchCount(5) // Prefetch 5 messages at a time
                    .ListenerCount(1); // Use a single listener

                // Publish messages to the pings queue
                opts.PublishMessage<Pong>()
                    .ToRabbitQueue("pongs");

                // Configure Rabbit MQ connection to the connection string
                // named "rabbit" from IConfiguration. This is *a* way to use
                // Wolverine + Rabbit MQ using Aspire
                opts.UseRabbitMqUsingNamedConnection("rabbit")
                    // Directs Wolverine to build any declared queues, exchanges, or
                    // bindings with the Rabbit MQ broker as part of bootstrapping time
                    .AutoProvision();

                // Override the application assembly to help Wolverine find its handlers
                opts.ApplicationAssembly = typeof(Program).Assembly;

                // Use "Auto" type load mode at development time, but "Static" any other time
                opts.OptimizeArtifactWorkflow();
            })
            .RunOaktonCommands(args);
    }
}
