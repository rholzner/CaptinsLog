using Captinslog.Domain;
using FlowCode;
using Microsoft.Extensions.DependencyInjection;

namespace Captinslog.Application;

public static class Setup
{
    public static IServiceCollection AddCaptinslogApp(this IServiceCollection services)
    {
        services.AddScoped<ICorrelationIdProvider, CorrelationIdProvider>();
        services.AddScoped<ILogEntryService, LogEntryService>();
        services.AddScoped<ILogEntryReader, LogEntryReader>();
        return services;
    }
}


