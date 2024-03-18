namespace Domain.Entities;

public class StoryCategory : DeleteEntity<Int64>
{
    public String Name { get; set; }

    public ICollection<Story> Stories { get; set; } = new HashSet<Story>();
}