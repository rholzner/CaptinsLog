using Captinslog.Domain;
using FlowCode;

namespace Captinslog.Application;
public interface ILogEntryReader
{
    ValueTask<OperationResult<IEnumerable<LogEntry>>> GetAllAsync(int skip, int take);
    ValueTask<OperationResult<IEnumerable<LogEntry>>> GetAllInStory(Guid storyId);
    ValueTask<OperationResult<IEnumerable<LogEntry>>> GetAllInCorrelationId(Guid correlationId);
    ValueTask<OperationResult<LogEntry>> Get(Guid logEntryId);
}
public class LogEntryReader : ILogEntryReader
{
    private readonly ILogEntryLoader _logEntryLoader;
    public LogEntryReader(ILogEntryLoader logEntryLoader)
    {
        _logEntryLoader = logEntryLoader;
    }
    public ValueTask<OperationResult<IEnumerable<LogEntry>>> GetAllAsync(int skip, int take)
    {
        return _logEntryLoader.GetAllAsync(skip, take);
    }
    public ValueTask<OperationResult<IEnumerable<LogEntry>>> GetAllInStory(Guid storyId)
    {
        return _logEntryLoader.GetAllInStory(storyId);
    }
    public ValueTask<OperationResult<IEnumerable<LogEntry>>> GetAllInCorrelationId(Guid correlationId)
    {
        return _logEntryLoader.GetAllInCorrelationId(correlationId);
    }
    public ValueTask<OperationResult<LogEntry>> Get(Guid logEntryId)
    {
        return _logEntryLoader.Get(logEntryId);
    }
}