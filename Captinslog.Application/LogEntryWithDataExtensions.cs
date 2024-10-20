using Captinslog.Domain;
using FlowCode;

namespace Captinslog.Application;

public static class LogEntryWithDataExtensions
{
    public static LogEntry<T> WithData<T>(this LogEntry logEntry, T data)
    {
        return new LogEntry<T>
        {
            Message = logEntry.Message,
            Date = logEntry.Date,
            StoryId = logEntry.StoryId,
            CorrelationId = logEntry.CorrelationId,
            Tags = logEntry.Tags,
            Data = data,
            IsSuccess = logEntry.IsSuccess
        };
    }
}


