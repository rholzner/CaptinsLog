using Captinslog.Domain;

namespace Captinslog.Tests;

public class LogEntryTests
{
    [Fact]
    public void LogEntry_ShouldHaveRequiredProperties()
    {
        // Arrange
        var logEntry = LogEntry.Empty;

        // Act

        // Assert
        Assert.NotNull(logEntry.Message);
        Assert.NotNull(logEntry.Date);
        Assert.Equal(Guid.Empty, logEntry.StoryId);
        Assert.Equal(Guid.Empty, logEntry.CorrelationId);
        Assert.NotNull(logEntry.Tags);
    }

    [Fact]
    public void LogEntryT_ShouldInheritFromLogEntry()
    {
        // Arrange
        var logEntryT = LogEntry<object>.Empty;

        // Act

        // Assert
        Assert.NotNull(logEntryT.Message);
        Assert.NotNull(logEntryT.Date);
        Assert.Equal(Guid.Empty, logEntryT.StoryId);
        Assert.Equal(Guid.Empty, logEntryT.CorrelationId);
        Assert.NotNull(logEntryT.Tags);
        Assert.NotNull(logEntryT.Data);
    }
}
