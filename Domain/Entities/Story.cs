using System.ComponentModel.DataAnnotations;

namespace Domain.Entities;

public class Story : AuditEntity<Int64>
{
    public String Name { get; set; }
    [Range(0, 10)]
    public Double Rating { get; set; }
    public String? Description { get; set; }
    public String? Thumbnail { get; set; }
    public String? SourceDescription { get; set; }
    public String? Author { get; set; }
    public String? Voice { get; set; }
    
    public Boolean IsBook { get; set; }
    
    public Boolean IsStory { get; set; }
    
    public Int64 StoryCategoryId { get; set; }
    public virtual ICollection<Episode> Episodes { get; set; } = new HashSet<Episode>();

    public virtual ICollection<Wishlist> Wishlists { get; set; } =  new HashSet<Wishlist>();

    public virtual ICollection<Comment> Comments { get; set; } = new HashSet<Comment>();

    public virtual ICollection<History> Histories { get; set; } = new HashSet<History>();
    
    public StoryCategory StoryCategory { get; set; }
}