using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities;

namespace Domain.RepositoryInterfaces
{
    public interface IWishlistRepository
    {
        Task<IEnumerable<Wishlist>> getStoriesUserWishList(String ProviderToken);

        void Insert(Wishlist wishlist);
        void Update(Wishlist wishlist);
        void Delete(Wishlist wishlist);
    }
}
