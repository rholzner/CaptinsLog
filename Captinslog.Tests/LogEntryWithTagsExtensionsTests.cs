using Captinslog.Application;
using Captinslog.Domain;

namespace Captinslog.Tests;
public class LogEntryWithTagsExtensionsTests
{
    [Fact]
    public void WithTags_ShouldReturnLogEntryWithTags()
    {
        // Arrange
        var logEntry = new LogEntry()
        {
            Message = "Test message",
            Date = DateTime.Now,
            StoryId = Guid.NewGuid(),
            CorrelationId = Guid.NewGuid(),
            Tags = Array.Empty<string>(),
            IsSuccess = true
        };
        var tags = new string[] { "tag1", "tag2" };

        // Act
        var result = logEntry.WithTags(tags);

        // Assert
        Assert.Equal(logEntry.Message, result.Message);
        Assert.Equal(logEntry.Date, result.Date);
        Assert.Equal(logEntry.StoryId, result.StoryId);
        Assert.Equal(logEntry.CorrelationId, result.CorrelationId);
        Assert.Equal(tags, result.Tags);
    }
}
