using Confluent.Kafka;
using Microsoft.EntityFrameworkCore;

namespace Topic.QueryService.Api;

public static class DependencyInjection
{
    public static IServiceCollection AddQueryServices(
        this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContextWithFactory(configuration);
        
        var consumerConfig = new ConsumerConfig();
        configuration.GetSection(nameof(ConsumerConfig)).Bind(consumerConfig);
        services.AddSingleton<ConsumerConfig>(consumerConfig);

        services.AddScoped<IKafkaEventSubscriber, KafkaEventSubscriber>();
        services.AddHostedService<KafkaEventConsumerBackgroundService>();
        services.RegisterQueriesHandler();

        return services;
    }

    public static WebApplication UseApiServices(this WebApplication app)
    {
        DatabaseInitializer.Initialize(app);

        return app;
    }

    public static IServiceCollection AddDbContextWithFactory(
        this IServiceCollection services, IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("PgConnection")!;

        Action<DbContextOptionsBuilder> config = options => options
            .UseLazyLoadingProxies()
            .UseNpgsql(connectionString);

        services.AddSingleton<DbContextFactory>(new DbContextFactory(config));
        services.AddDbContext<ApplicationContext>(config);
        services.AddScoped<ITopicStorage, TopicStorage>();
        services.AddScoped<ICommentStorage, CommentStorage>();
        services.AddScoped<IQueryEventHandler, QueryEventHandler>(); 

        return services;
    }
    
    private static IServiceCollection RegisterQueriesHandler(
        this IServiceCollection services)
    {

        services.AddScoped<ITopicQueryHandler, TopicQueryHandler>();

        services.AddScoped<IQueryDispatcher<TopicEntity>>(provider =>
        {
            var dispatcher = new QueryDispatcher();

            var commandTopicHandler = provider
                .GetRequiredService<ITopicQueryHandler>();

            dispatcher.RegisterHandler<GetTopicsQuery>(command =>
            {
                return commandTopicHandler.HandleAsync(command);
            });
            dispatcher.RegisterHandler<GetTopicByIdQuery>(command =>
            {
                return commandTopicHandler.HandleAsync(command);
            });
            dispatcher.RegisterHandler<GetTopicsByAuthorNameQuery>(command =>
            {
                return commandTopicHandler.HandleAsync(command);
            });
            dispatcher.RegisterHandler<GetTopicsWithCommentsQuery>(command =>
            {
                return commandTopicHandler.HandleAsync(command);
            });
            dispatcher.RegisterHandler<GetTopicsWithLikesQuery>(command =>
            {
                return commandTopicHandler.HandleAsync(command);
            });
            return dispatcher;
        });

        return services;
    }
}