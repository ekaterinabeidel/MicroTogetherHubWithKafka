using System.Text.Json;
using Confluent.Kafka;
using Microsoft.Extensions.Logging;

namespace Topic.QueryService.Infrastructure.Consumers;

public class KafkaEventSubscriber(
    ConsumerConfig config,
    IQueryEventHandler eventHandler,
    ILogger<KafkaEventSubscriber> logger,
    int retryIntervalSeconds = 3) : IKafkaEventSubscriber
{
    public async Task ConsumeAsync(string topic, CancellationToken ct)
    {
        using var consumer = new ConsumerBuilder<string, string>(config)
            .SetKeyDeserializer(Deserializers.Utf8)
            .SetValueDeserializer(Deserializers.Utf8)
            .Build();

        var adminConfig = new AdminClientConfig
        {
            BootstrapServers = config.BootstrapServers
        };

        var result = await WaitForTopicAsync(topic, adminConfig, ct);

        if (result)
        {
            logger.LogInformation($"Топик {topic} существует. Начинаем подписку");
            consumer.Subscribe(topic);
        }

        while (!ct.IsCancellationRequested)
        {
            try
            {
                var consumeResult = consumer.Consume(ct);
                if (consumeResult?.Message is null) continue;
                logger.LogInformation($"Получено сообщение: {consumeResult.Message.Value}");
                var options = new JsonSerializerOptions
                {
                    Converters = { new EventJsonConverter() }
                };
                var baseEvent = JsonSerializer
                    .Deserialize<BaseEvent>(consumeResult.Message.Value, options);

                if (baseEvent is null) continue;

                var handlerMethod = eventHandler.GetType().GetMethod("On", [baseEvent.GetType()]);

                if (handlerMethod is null)
                {
                    const string errorMessage = "Не удалось найти метод обработчика события";
                    throw new ArgumentNullException(nameof(handlerMethod), errorMessage);
                }
                await Task.Run(() => handlerMethod.Invoke(eventHandler, [baseEvent]), ct);
                consumer.Commit(consumeResult);
            }
            catch (OperationCanceledException)
            {
                break;
            }
            catch (Exception ex)
            {
                logger.LogInformation($"Ошибка при обработке сообщения: {ex.Message}");
            }
        }
    }

    private async Task<bool> WaitForTopicAsync(
        string topic,
        AdminClientConfig adminConfig,
        CancellationToken ct)
    {

        bool topicExists = false;
        while (!topicExists)
        {
            try
            {
                using var adminClient = new AdminClientBuilder(adminConfig).Build();
                var metadata = await Task.Run(() => adminClient.
                    GetMetadata(TimeSpan.FromSeconds(retryIntervalSeconds)), ct);

                topicExists = metadata.Topics
                    .Any(t => t.Topic == topic && t.Error.Code == ErrorCode.NoError);

                if (topicExists)
                {
                    return true;
                }

                logger.LogInformation(@$"Топик {topic} не найден. 
                    Повторная проверка через {retryIntervalSeconds} секунд");

                await Task.Delay(TimeSpan.FromSeconds(retryIntervalSeconds), ct);
            }
            catch (OperationCanceledException ex)
            {
                logger.LogInformation($"Ошибка при проверке топика {topic}: {ex.Message}");
                return false;
            }
            catch (Exception ex)
            {
                logger.LogInformation($"Ошибка при проверке топика {topic}: {ex.Message}");
                await Task.Delay(TimeSpan.FromSeconds(retryIntervalSeconds), ct);
            }
        }
        return false;
    }
}