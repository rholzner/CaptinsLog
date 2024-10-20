using Captinslog.Domain;
using FlowCode;

namespace Captinslog.Application;

public static class OperationResultToLogEntryExtensions
{
    public static LogEntry ToLogEntry(this OperationResult operationResult, Guid storyId, Guid correlationId, string message, DateTime dateTime)
    {
        return new LogEntry
        {
            IsSuccess = operationResult.IsSuccess,
            Message = operationResult.ToLogMessage(message),
            Date = dateTime,
            StoryId = storyId,
            CorrelationId = correlationId,
        };
    }

    public static LogEntry<T> ToLogEntry<T>(this OperationResult<T> operationResult, Guid storyId, Guid correlationId, string message, DateTime dateTime)
    {
        var log = operationResult.ToLogEntry(storyId, correlationId, message, dateTime);
        return log.WithData(operationResult.Data);
    }

    public static string ToLogMessage(this OperationResult operationResult, string message)
    {
        return operationResult.IsSuccess ? message : $"{message} - {operationResult?.Exception?.Message ?? "error"}";
    }
}

