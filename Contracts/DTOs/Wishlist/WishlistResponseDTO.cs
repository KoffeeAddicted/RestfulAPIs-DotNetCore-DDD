using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Contracts.DTOs.Stories;
using Services.DTOs.Episodes;

namespace Contracts.DTOs.Wishlist
{
    public class WishlistResponseDTO
    {
        public Int64 Id { get; set; }
        public String ProviderToken { get; set; }
        public Int64 StoryId { get; set; }
        


        public StoryResponseDTO Story { get; set; }
    }
}
