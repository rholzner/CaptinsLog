using Captinslog.Application;
using FlowCode;

namespace Captinslog.Tests;

public class OperationResultToLogEntryExtensionsTests
{
    [Fact]
    public void ToLogEntry_ReturnsLogEntryWithCorrectValues()
    {
        // Arrange
        var operationResult = OperationResult.Success();
        var storyId = Guid.NewGuid();
        var correlationId = Guid.NewGuid();
        var message = "Test message";
        var dateTime = DateTime.Now;

        // Act
        var logEntry = operationResult.ToLogEntry(storyId, correlationId, message, dateTime);

        // Assert
        Assert.Equal(operationResult.IsSuccess, logEntry.IsSuccess);
        Assert.Equal(message, logEntry.Message);
        Assert.Equal(dateTime, logEntry.Date);
        Assert.Equal(storyId, logEntry.StoryId);
        Assert.Equal(correlationId, logEntry.CorrelationId);
    }

    [Fact]
    public void ToLogEntry_WithGenericOperationResult_ReturnsLogEntryWithCorrectValues()
    {
        // Arrange
        var operationResult = OperationResult<string>.Success("Test data");

        var storyId = Guid.NewGuid();
        var correlationId = Guid.NewGuid();
        var message = "Test message";
        var dateTime = DateTime.Now;

        // Act
        var logEntry = operationResult.ToLogEntry(storyId, correlationId, message, dateTime);

        // Assert
        Assert.Equal(operationResult.IsSuccess, logEntry.IsSuccess);
        Assert.Equal(message, logEntry.Message);
        Assert.Equal(dateTime, logEntry.Date);
        Assert.Equal(storyId, logEntry.StoryId);
        Assert.Equal(correlationId, logEntry.CorrelationId);
        Assert.Equal(operationResult.Data, logEntry.Data);
    }

    [Fact]
    public void ToLogMessage_ReturnsMessageWhenOperationResultIsSuccess()
    {
        // Arrange
        var operationResult = OperationResult.Success();
        var message = "Test message";

        // Act
        var logMessage = operationResult.ToLogMessage(message);

        // Assert
        Assert.Equal(message, logMessage);
    }

    [Fact]
    public void ToLogMessage_ReturnsMessageWithErrorWhenOperationResultIsFailure()
    {
        // Arrange
        var operationResult = OperationResult.Failure(new Exception("Test exception"));
        
        var message = "Test message";

        // Act
        var logMessage = operationResult.ToLogMessage(message);

        // Assert
        Assert.Equal($"{message} - {operationResult.Exception.Message}", logMessage);
    }
}
