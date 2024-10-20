namespace Captinslog.Infrastructure.Entities;

public class LogEntryEntity
{
    public Guid Id { get; set; }
    public DateTime Created { get; set; }

    public required string Message { get; set; }
    public required bool IsSuccess { get; set; }

    public required Guid CorrelationId { get; set; }
    public required CorrelationEntity Correlation { get; set; }
    public IList<TagEntity> Tags { get; set; }

    /// <summary>
    /// The data is a json string that can be used to store additional information about the log entry.
    /// </summary>
    public string? Data { get; set; }
}
