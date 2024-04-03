using System.ComponentModel.DataAnnotations;

namespace Domain.Entities;

public class Wishlist : AuditEntity<Int64>
{
    public String ProviderToken { get; set; }

    public Int64 StoryId { get; set; }

    public virtual Story Story { get; set; }
    public virtual User User { get; set; }
}