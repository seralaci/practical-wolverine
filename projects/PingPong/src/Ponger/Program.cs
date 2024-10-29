using Microsoft.Extensions.Hosting;
using Oakton;
using Wolverine;
using Wolverine.MemoryPack;
using Wolverine.Transports.Tcp;

namespace Ponger;

public static class Program
{
    public static async Task<int> Main(string[] args)
    {
        return await Host
            .CreateDefaultBuilder(args)
            .UseWolverine(opts =>
            {
                // Using Wolverine's built in TCP transport
                // listen to incoming messages at port 5581
                opts.ListenAtPort(5581);

                // Make MemoryPack the default serializer throughout this application
                opts.UseMemoryPackSerialization();

                // Override the application assembly to help Wolverine find its handlers
                opts.ApplicationAssembly = typeof(Program).Assembly;
            })
            .RunOaktonCommands(args);
    }
}
