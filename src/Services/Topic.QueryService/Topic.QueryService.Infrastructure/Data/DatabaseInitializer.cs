using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Topic.QueryService.Infrastructure.Data;

public static class DatabaseInitializer
{
    public static void Initialize(IHost host)
    {
        using (var scope = host.Services.CreateScope())
        {
            var services = scope.ServiceProvider;

            var context = services.GetRequiredService<ApplicationContext>();

            context.Database.EnsureCreated();
        }
    }
}