using FlowCode;

namespace Captinslog.Domain;

public interface ILogEntryRepository
{
    OperationResult Add(LogEntry logEntry);
    OperationResult Add<T>(LogEntry<T> logEntry);

    ValueTask<OperationResult> AddAsync(LogEntry logEntry);
    ValueTask<OperationResult> AddAsync<T>(LogEntry<T> logEntry);
}
