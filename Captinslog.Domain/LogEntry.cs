namespace Captinslog.Domain;

public class LogEntry
{
    public required string Message { get; set; }
    public required DateTime Date { get; set; }

    /// <summary>
    /// The story id is used to group log entries together. This is useful when you want to see all log entries for a specific story.
    /// </summary>
    public required Guid StoryId { get; set; }

    /// <summary>
    /// The correlation id is used to group log entries together. This is useful when you want to see all log entries for a specific request.
    /// </summary>
    public required Guid CorrelationId { get; set; }
    public IEnumerable<string> Tags { get; set; }

    public required bool IsSuccess { get; set; }

    public static LogEntry Empty => new LogEntry() { IsSuccess = true, CorrelationId = Guid.Empty, Message = string.Empty, Date = DateTime.MinValue, StoryId = Guid.Empty, Tags = Array.Empty<string>() };
}

public class LogEntry<T> : LogEntry
{
    public LogEntry()
    {

    }

    public required T Data { get; set; }

    public static new LogEntry<T> Empty => new LogEntry<T>() { IsSuccess = true, CorrelationId = Guid.Empty, Message = string.Empty, Date = DateTime.MinValue, StoryId = Guid.Empty, Tags = Array.Empty<string>(), Data = default! };
}
