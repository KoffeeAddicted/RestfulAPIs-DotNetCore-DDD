using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Contracts.DTOs.Users;
using Contracts.DTOs.Wishlist;

namespace Services.Absractions
{
    public interface IWishlistService
    {
        Task<WishlistResponseDTO> AddStoryUserWishList(StoryUserWishlistCreate storyUserWishlistCreate);

        Task<WishlistResponseDTO?> GetStoryByWishlist(String ProviderToken, Int64 StoryId);

        Task<WishlistResponseDTO> DeleteStoryWishList(StoryUserWishlistCreate storyUserWishlistCreate);
        Task<IEnumerable<WishlistResponseDTO>> GetUserWishList(String ProviderToken);
    }
}
