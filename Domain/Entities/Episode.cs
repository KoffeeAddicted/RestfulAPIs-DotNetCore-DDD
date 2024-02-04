namespace Domain.Entities;

public class Episode : AuditEntity<Int64>
{
    public Int32 OrderNumber { get; set; }
    public Int64 StoryId { get; set; }
    public virtual Story Story { get; set; }
    public virtual Audio Audio { get; set; }
}