namespace Captinslog.Infrastructure.Entities;

public class TagEntity
{
    public required Guid Id { get; set; }
    public required string Name { get; set; }
    public required string SanitizedName { get; set; }
    public required DateTime Created { get; set; }
}
