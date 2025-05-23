

using Carter;
using Confluent.Kafka;
using Core.KafkaProducer;
using Topic.CommandService.Infrastructure.KafkaProducer;
using Topic.CommandService.Infrastructure.Services;

namespace Topic.CommandService.Api;

public static class DependencyInjection
{
    public static IServiceCollection AddCommandServices(
        this IServiceCollection services, IConfiguration configuration
    )
    {
        var connectionString = configuration.GetConnectionString("PgConnection")!;
        services
            .AddMarten(opt => opt.Connection(connectionString))
            .UseLightweightSessions();

        services.AddScoped<IEventStorage, EventStorage>();
        services.AddScoped<IEventService, EventService>();
        services.AddScoped<IEventHandler<ContentAggregate>, EventHandlerImpl>();
        services.AddScoped<ICommentCommandHandler, CommentCommandHandler>();
        services.AddScoped<ITopicCommandHandler, TopicCommandHandler>();
        services.RegisterCommandHandler(); 
        services.Configure<ProducerConfig>(configuration.GetSection("KafkaConfig"));
        services.AddScoped<IEventKafkaProducer, EventKafkaProducer>();
        services.AddCarter();
        return services;
    }
    
    private static IServiceCollection RegisterCommandHandler(
        this IServiceCollection services)
    {
        services.AddScoped<ICommandDispatcher>(provider =>
        {
            var dispatcher = new CommandDispatcher();

            var commandCommentHandler = provider.GetRequiredService<ICommentCommandHandler>();
            dispatcher.RegisterHandler<CreateCommentCommand>(command =>
            {
                return commandCommentHandler.HandleAsync(command, CancellationToken.None);
            });
            dispatcher.RegisterHandler<UpdateCommentCommand>(command =>
            {
                return commandCommentHandler.HandleAsync(command, CancellationToken.None);
            });
            dispatcher.RegisterHandler<RemoveCommentCommand>(command =>
            {
                return commandCommentHandler.HandleAsync(command, CancellationToken.None);
            });

            var commandTopicHandler = provider.GetRequiredService<ITopicCommandHandler>();
            dispatcher.RegisterHandler<CreateTopicCommand>(command =>
            {
                return commandTopicHandler.HandleAsync(command, CancellationToken.None);
            });
            dispatcher.RegisterHandler<RemoveTopicCommand>(command =>
            {
                return commandTopicHandler.HandleAsync(command, CancellationToken.None);
            });
            dispatcher.RegisterHandler<UpdateTopicCommand>(command =>
            {
                return commandTopicHandler.HandleAsync(command, CancellationToken.None);
            });
            dispatcher.RegisterHandler<LikeTopicCommand>(command =>
            {
                return commandTopicHandler.HandleAsync(command, CancellationToken.None);
            });

            return dispatcher;
        });

        return services;
    }
    
    public static WebApplication UseApiServices(this WebApplication app)  // ❗️
    {
        app.MapCarter();
        return app;
    }
}

