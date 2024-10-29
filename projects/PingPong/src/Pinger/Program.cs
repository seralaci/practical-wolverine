using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Oakton;
using Pinger.BackgroundServices;
using Pinger.Messages;
using Wolverine;
using Wolverine.MemoryPack;
using Wolverine.Transports.Tcp;

namespace Pinger;

public static class Program
{
    public static async Task<int> Main(string[] args)
    {
        return await Host
            .CreateDefaultBuilder(args)
            .UseWolverine(opts =>
            {
                // Using Wolverine's built in TCP transport
                // listen to incoming messages at port 5580
                opts.ListenAtPort(5580);

                // route all Ping messages to port 5581
                opts.PublishMessage<Ping>().ToPort(5581);

                // Make MemoryPack the default serializer throughout this application
                opts.UseMemoryPackSerialization();

                // Registering the hosted service here, but could do
                // that with a separate call to IHostBuilder.ConfigureServices()
                opts.Services.AddHostedService<PingerService>();

                // Override the application assembly to help Wolverine find its handlers
                opts.ApplicationAssembly = typeof(Program).Assembly;
            })
            .RunOaktonCommands(args);
    }
}
