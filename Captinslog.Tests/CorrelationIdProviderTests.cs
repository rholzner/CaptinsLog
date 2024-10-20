using Captinslog.Application;
using System;
using Xunit;

namespace Captinslog.Tests;

public class CorrelationIdProviderTests
{
    [Fact]
    public void BeginScope_ShouldReturnValidCorrelationId()
    {
        // Arrange
        var correlationIdProvider = new CorrelationIdProvider();

        // Act
        var result = correlationIdProvider.BeginScope();

        // Assert
        Assert.True(result.IsSuccess);
        Assert.NotEqual(Guid.Empty, result.Data);
    }

    [Fact]
    public void Dispose_ShouldResetCorrelationId()
    {
        // Arrange
        var correlationIdProvider = new CorrelationIdProvider();

        // Act
        correlationIdProvider.BeginScope();
        correlationIdProvider.Dispose();

        // Assert
        Assert.Equal(Guid.Empty, correlationIdProvider._correlationId);
    }
}
