using Microsoft.EntityFrameworkCore;

namespace Topic.QueryService.Infrastructure.Data;

public class DbContextFactory(Action<DbContextOptionsBuilder> configureDbContext)
{
    public ApplicationContext CreateDbContext()
    {
        DbContextOptionsBuilder<ApplicationContext> builder = new();
        configureDbContext(builder);

        return new ApplicationContext(builder.Options);
    }
}