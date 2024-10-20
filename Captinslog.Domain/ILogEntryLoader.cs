using FlowCode;

namespace Captinslog.Domain;

public interface ILogEntryLoader
{
    ValueTask<OperationResult<IEnumerable<LogEntry>>> GetAllAsync(int skip, int take);
    ValueTask<OperationResult<IEnumerable<LogEntry>>> GetAllInStory(Guid storyId);
    ValueTask<OperationResult<IEnumerable<LogEntry>>> GetAllInCorrelationId(Guid correlationId);
    ValueTask<OperationResult<LogEntry>> Get(Guid logEntryId);
}

