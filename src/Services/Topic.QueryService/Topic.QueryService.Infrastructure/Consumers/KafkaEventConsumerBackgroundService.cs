using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace Topic.QueryService.Infrastructure.Consumers;

public class KafkaEventConsumerBackgroundService(
    ILogger<KafkaEventConsumerBackgroundService> logger,
    IServiceProvider serviceProvider,
    IConfiguration configuration) : IHostedService
{
    public Task StartAsync(CancellationToken ct)
    {
        logger.LogInformation("Служба потребителей событий запущена");
        using IServiceScope scope = serviceProvider.CreateScope();

        var eventConsumer = scope
            .ServiceProvider
            .GetRequiredService<IKafkaEventSubscriber>();

        var topic = configuration.GetValue<string>("Kafka:Topic")!;
        Task.Run(() => eventConsumer.ConsumeAsync(topic, ct), ct);

        return Task.CompletedTask;
    }

    public Task StopAsync(CancellationToken ct)
    {
        logger.LogInformation("Служба потребителей событий остановлена");
        return Task.CompletedTask;
    }
}