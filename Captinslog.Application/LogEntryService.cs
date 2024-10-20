using Captinslog.Domain;
using FlowCode;

namespace Captinslog.Application;
public interface ILogEntryService
{
    OperationResult Add(LogEntry logEntry);
    OperationResult Add<T>(LogEntry<T> logEntry);
    OperationResult Add(Guid storyId, string message, params IEnumerable<string> tags);
    OperationResult Add<T>(Guid storyId, string message,T data, params IEnumerable<string> tags);

    OperationResult Add(Guid storyId, string message, bool success, params IEnumerable<string> tags);
    OperationResult Add<T>(Guid storyId, string message, bool success, T data, params IEnumerable<string> tags);
    OperationResult Add(OperationResult operationResult, Guid storyId, string message, params IEnumerable<string> tags);
    OperationResult Add<T>(OperationResult<T> operationResult, Guid storyId, string message, params IEnumerable<string> tags);


    ValueTask<OperationResult> AddAsync(LogEntry logEntry);
    ValueTask<OperationResult> AddAsync<T>(LogEntry<T> logEntry);
    ValueTask<OperationResult> AddAsync(Guid storyId, string message, bool success, params IEnumerable<string> tags);
    ValueTask<OperationResult> AddAsync<T>(Guid storyId, string message, bool success, T data, params IEnumerable<string> tags);
    ValueTask<OperationResult> AddAsync(OperationResult operationResult, Guid storyId, string message, params IEnumerable<string> tags);
    ValueTask<OperationResult> AddAsync<T>(OperationResult<T> operationResult, Guid storyId, string message, params IEnumerable<string> tags);
}

/// <summary>
/// Should be scoped to the lifetime of a request
/// </summary>
public class LogEntryService : ILogEntryService
{
    private readonly ILogEntryRepository _logEntryRepository;
    private readonly ICorrelationIdProvider _correlationIdProvider;

    public LogEntryService(ILogEntryRepository logEntryRepository, ICorrelationIdProvider correlationIdProvider)
    {
        _logEntryRepository = logEntryRepository;
        _correlationIdProvider = correlationIdProvider;
    }
    public OperationResult Add(Guid storyId, string message, bool success, params IEnumerable<string> tags)
    {
        var correlationId = _correlationIdProvider.BeginScope();
        return correlationId.OnSuccess(
            cor =>
            {
                var logEntry = new LogEntry
                {
                    StoryId = storyId,
                    Message = message,
                    IsSuccess = success,
                    Date = DateTime.UtcNow,
                    CorrelationId = cor,
                    Tags = tags
                };
                return _logEntryRepository.Add(logEntry);
            }
        );
    }

    public OperationResult Add<T>(Guid storyId, string message, bool success, T data, params IEnumerable<string> tags)
    {
        var correlationId = _correlationIdProvider.BeginScope();
        return correlationId.OnSuccess(
            result =>
            {
                var logEntry = new LogEntry<T>
                {
                    StoryId = storyId,
                    Message = message,
                    IsSuccess = success,
                    Date = DateTime.UtcNow,
                    CorrelationId = result,
                    Data = data,
                    Tags = tags
                };
                return _logEntryRepository.Add(logEntry);
            }
        );
    }

    public OperationResult Add(OperationResult operationResult, Guid storyId, string message, params IEnumerable<string> tags)
    {
        var correlationId = _correlationIdProvider.BeginScope();
        return correlationId.OnSuccess(
            result =>
            {
                var logEntry = operationResult.ToLogEntry(storyId, result, message, DateTime.UtcNow);
                logEntry.WithTags(tags);
                return _logEntryRepository.Add(logEntry);
            }
        );
    }

    public OperationResult Add<T>(OperationResult<T> operationResult, Guid storyId, string message, params IEnumerable<string> tags)
    {
        var correlationId = _correlationIdProvider.BeginScope();
        return correlationId.OnSuccess(
            result =>
            {
                var logEntry = operationResult.ToLogEntry(storyId, result, message, DateTime.UtcNow);
                logEntry.WithTags(tags);
                return _logEntryRepository.Add(logEntry);
            }
        );
    }

    public OperationResult Add(LogEntry logEntry)
    {
        var correlationId = _correlationIdProvider.BeginScope();
        return correlationId.OnSuccess(
            result =>
            {
                logEntry.CorrelationId = result;
                return _logEntryRepository.Add(logEntry);
            }
        );
    }

    public OperationResult Add<T>(LogEntry<T> logEntry)
    {
        var correlationId = _correlationIdProvider.BeginScope();
        return correlationId.OnSuccess(
            result =>
            {
                logEntry.CorrelationId = result;
                return _logEntryRepository.Add(logEntry);
            }
        );
    }

    public OperationResult Add(Guid storyId, string message, params IEnumerable<string> tags)
    {
        var correlationId = _correlationIdProvider.BeginScope();
        return correlationId.OnSuccess(
            result =>
            {
                var logEntry = new LogEntry
                {
                    StoryId = storyId,
                    Message = message,
                    IsSuccess = true,
                    Date = DateTime.UtcNow,
                    CorrelationId = result,
                    Tags = tags
                };
                return _logEntryRepository.Add(logEntry);
            }
        );
    }

    public OperationResult Add<T>(Guid storyId, string message, T data, params IEnumerable<string> tags)
    {
        var correlationId = _correlationIdProvider.BeginScope();
        return correlationId.OnSuccess(
            result =>
            {
                var logEntry = new LogEntry<T>
                {
                    StoryId = storyId,
                    Message = message,
                    IsSuccess = true,
                    Date = DateTime.UtcNow,
                    CorrelationId = result,
                    Tags = tags,
                    Data = data
                };
                return _logEntryRepository.Add(logEntry);
            }
        );
    }

    public async ValueTask<OperationResult> AddAsync(LogEntry logEntry)
    {
        var correlationId = _correlationIdProvider.BeginScope();
        return await correlationId.OnSuccessAsync(
            async result =>
            {
                logEntry.CorrelationId = result;
                return await _logEntryRepository.AddAsync(logEntry);
            }
        );
    }

    public async ValueTask<OperationResult> AddAsync<T>(LogEntry<T> logEntry)
    {
        var correlationId = _correlationIdProvider.BeginScope();
        return await correlationId.OnSuccessAsync(
            async result =>
            {
                logEntry.CorrelationId = result;
                return await _logEntryRepository.AddAsync(logEntry);
            }
        );
    }

    public async ValueTask<OperationResult> AddAsync(Guid storyId, string message, bool success, params IEnumerable<string> tags)
    {
        var correlationId = _correlationIdProvider.BeginScope();
        return await correlationId.OnSuccessAsync(
            async result =>
            {
                var logEntry = new LogEntry
                {
                    StoryId = storyId,
                    Message = message,
                    IsSuccess = success,
                    Date = DateTime.UtcNow,
                    CorrelationId = result,
                    Tags = tags
                };
                return await _logEntryRepository.AddAsync(logEntry);
            }
        );
    }

    public async ValueTask<OperationResult> AddAsync<T>(Guid storyId, string message, bool success, T data, params IEnumerable<string> tags)
    {
        var correlationId = _correlationIdProvider.BeginScope();
        return await correlationId.OnSuccessAsync(
            async result =>
            {
                var logEntry = new LogEntry<T>
                {
                    StoryId = storyId,
                    Message = message,
                    IsSuccess = success,
                    Date = DateTime.UtcNow,
                    CorrelationId = result,
                    Data = data,
                    Tags = tags
                };
                return await _logEntryRepository.AddAsync(logEntry);
            }
        );
    }

    public async ValueTask<OperationResult> AddAsync(OperationResult operationResult, Guid storyId, string message, params IEnumerable<string> tags)
    {
        var correlationId = _correlationIdProvider.BeginScope();
        return await correlationId.OnSuccessAsync(
            async result =>
            {
                var logEntry = operationResult.ToLogEntry(storyId, result, message, DateTime.UtcNow);
                logEntry.WithTags(tags);
                return await _logEntryRepository.AddAsync(logEntry);
            }
        );
    }

    public async ValueTask<OperationResult> AddAsync<T>(OperationResult<T> operationResult, Guid storyId, string message, params IEnumerable<string> tags)
    {
        var correlationId = _correlationIdProvider.BeginScope();
        return await correlationId.OnSuccessAsync(
            async result =>
            {
                var logEntry = operationResult.ToLogEntry(storyId, result, message, DateTime.UtcNow);
                logEntry.WithTags(tags);
                return await _logEntryRepository.AddAsync(logEntry);
            }
        );
    }
}


