namespace Captinslog.Infrastructure.Entities;

public class StoryEntity
{
    public Guid Id { get; set; }
    public required Guid StoryId { get; set; }
    public DateTime Created { get; set; }

    public IList<CorrelationEntity> Correlations { get; set; }
}
