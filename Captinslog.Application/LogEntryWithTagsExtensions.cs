using Captinslog.Domain;

namespace Captinslog.Application;

public static class LogEntryWithTagsExtensions
{
    public static LogEntry WithTags(this LogEntry logEntry, params IEnumerable<string> tags)
    {
        logEntry.Tags = tags;
        return logEntry;
    }
}

