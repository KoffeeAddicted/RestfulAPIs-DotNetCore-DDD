namespace Domain.Entities;

public class Tag : DeleteEntity<Int64>
{
    public String Name { get; set; }

    public virtual ICollection<StoryTag> StoryTags { get; set; } = new HashSet<StoryTag>();
}