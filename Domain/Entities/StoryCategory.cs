namespace Domain.Entities;

public class StoryCategory : DeleteEntity<Int64>
{
    public String Name { get; set; }
}