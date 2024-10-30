using JasperFx.Core;
using Wolverine;
using Pinger.Messages;

namespace Pinger.BackgroundServices;

internal sealed class PingerService(IServiceScopeFactory scopeFactory) : BackgroundService
{
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        var counter = 1;

        using var serviceScope = scopeFactory.CreateScope();
        var bus = serviceScope.ServiceProvider.GetRequiredService<IMessageBus>();

        while (!stoppingToken.IsCancellationRequested)
        {
            try
            {
                var ping = new Ping(counter++);
                await bus.SendAsync(ping);
                await Task.Delay(500.Milliseconds(), stoppingToken);
            }
            catch (TaskCanceledException)
            {
                return;
            }
        }
    }
}
