using Domain.Entities;

namespace Domain;

public class User : DeleteEntity<Int64>
{
    public String ProviderToken { get; set; }

    public String? Email { get; set; }
    public String? Password { get; set; }
    public Boolean IsAdmin { get; set; } = false;

    public String? ProfilePicture { get; set; }


    public virtual ICollection<Wishlist> Wishlists { get; set; }

    public virtual ICollection<History> Histories { get; set; }
}