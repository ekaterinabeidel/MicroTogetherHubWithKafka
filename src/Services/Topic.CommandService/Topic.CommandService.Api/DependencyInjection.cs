using Core.Events.Dao;
using Core.Handlers;
using Core.Services;
using Marten;
using Topic.CommandService.Domain.Aggregates;
using Topic.CommandService.Infrastructure.Handlers;

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

        return services;
    }
}