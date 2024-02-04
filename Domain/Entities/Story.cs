using System.ComponentModel.DataAnnotations;

namespace Domain.Entities;

public class Story : AuditEntity<Int64>
{
    public String Name { get; set; }
    [Range(0, 10)]
    public Double Rating { get; set; }
    public String Description { get; set; }
    public String Thumbnail { get; set; }
    public String SourceDescription { get; set; }
    public virtual ICollection<Episode> Episodes { get; set; } = new HashSet<Episode>();
}