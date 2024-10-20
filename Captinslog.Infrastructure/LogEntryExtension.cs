using Captinslog.Domain;
using Captinslog.Infrastructure.Entities;

namespace Captinslog.Infrastructure;

public static class LogEntryExtension
{
    public static LogEntry ToLogEntry(this LogEntryEntity entity)
    {
        return new LogEntry
        {
            CorrelationId = entity?.Correlation?.CorrelationId ?? Guid.Empty,
            Date = entity?.Created ?? DateTime.MinValue,
            IsSuccess = entity?.IsSuccess ?? false,
            Message = entity?.Message ?? string.Empty,
            StoryId = entity?.Correlation?.Story?.StoryId ?? Guid.Empty,
            Tags = entity?.Tags?.Select(x => x.Name) ?? Enumerable.Empty<string>()
        };
    }
}
