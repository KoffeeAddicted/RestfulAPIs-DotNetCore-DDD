namespace Domain.Entities;

public class StoryTag
{
    public Int64 StoryId { get; set; }
    public Int64 TagId { get; set; }
    public virtual Story Story { get; set; }
    public virtual Tag Tag { get; set; }
}