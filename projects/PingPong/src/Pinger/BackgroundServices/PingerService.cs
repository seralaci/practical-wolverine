using JasperFx.Core;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Wolverine;
using Pinger.Messages;

namespace Pinger.BackgroundServices;

internal sealed class PingerService(ILogger<PingerService> logger, IServiceScopeFactory scopeFactory) : BackgroundService
{
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        var counter = 1;

        using var serviceScope = scopeFactory.CreateScope();
        var bus = serviceScope.ServiceProvider.GetRequiredService<IMessageBus>();

        while (!stoppingToken.IsCancellationRequested)
        {
            var ping = new Ping(counter++);

            logger.LogInformation("Sending Ping #{Ping}", ping);

            await bus.PublishAsync(ping);

            await Task.Delay(2.Seconds(), stoppingToken);
        }
    }
}
