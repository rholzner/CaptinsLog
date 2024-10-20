using Captinslog.Domain;
using Microsoft.Extensions.DependencyInjection;

namespace Captinslog.Infrastructure;

public static class Setup
{
    public static IServiceCollection AddLogEntryRepository(this IServiceCollection services)
    {
        services.AddScoped<ILogEntryLoader, LogEntryLoader>();
        services.AddScoped<ILogEntryRepository, LogEntryRepository>();
        services.AddScoped<ILogEntryDatabaseContextHelper, LogEntryDatabaseContextHelper>();
        services.AddSingleton<IJsonSerializer, JsonSerializer>();
        services.AddDbContext<LogEntryDatabaseContext>();

        return services;
    }
}