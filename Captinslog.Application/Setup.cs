using Microsoft.Extensions.DependencyInjection;

namespace Captinslog.Application;

public static class Setup
{
    public static void AddCaptinslogApp(this IServiceCollection services)
    {
        services.AddScoped<ICorrelationIdProvider, CorrelationIdProvider>();
        services.AddScoped<ILogEntryService, LogEntryService>();
    }
}