using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contracts.DTOs.Wishlist
{
    public class StoryUserWishlistCreate
    {
        public String ProviderToken { get; set; }
        public Int64 StoryId { get; set; }
    }
}
