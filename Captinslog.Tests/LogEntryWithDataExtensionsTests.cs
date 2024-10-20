using Captinslog.Application;
using Captinslog.Domain;

namespace Captinslog.Tests;

public class LogEntryWithDataExtensionsTests
{
    [Fact]
    public void WithData_ShouldReturnLogEntryWithData()
    {
        // Arrange
        var logEntry = new LogEntry
        {
            Message = "Test message",
            Date = DateTime.Now,
            StoryId = Guid.NewGuid(),
            CorrelationId = Guid.NewGuid(),
            Tags = new string[] { "tag1", "tag2" },
            IsSuccess = true
        };
        var data = new TestData { Value = 42 };

        // Act
        var result = logEntry.WithData(data);

        // Assert
        Assert.IsType<LogEntry<TestData>>(result);
        Assert.Equal(logEntry.Message, result.Message);
        Assert.Equal(logEntry.Date, result.Date);
        Assert.Equal(logEntry.StoryId, result.StoryId);
        Assert.Equal(logEntry.CorrelationId, result.CorrelationId);
        Assert.Equal(logEntry.Tags, result.Tags);
        Assert.Equal(data, result.Data);
    }

    public class TestData
    {
        public int Value { get; set; }
    }
}
