using Core.Events.Dao;
using Core.Services;
using Marten;

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
        services.AddScoped<IEventService, EventService>(); // ❗️

        return services;
    }
}