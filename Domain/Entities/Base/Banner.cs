namespace Domain.Entities;

public class Banner : AuditEntity<long>
{
    public string Name { get; set; }
    public string Content { get; set; }
}