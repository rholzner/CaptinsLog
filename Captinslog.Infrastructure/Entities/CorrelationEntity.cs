namespace Captinslog.Infrastructure.Entities;

public class CorrelationEntity
{
    public Guid Id { get; set; }
    public required Guid CorrelationId { get; set; }
    public DateTime Created { get; set; }


    public required Guid StoryId { get; set; }
    public required StoryEntity Story { get; set; }
    public IList<LogEntryEntity> LogEntries { get; set; }
}
