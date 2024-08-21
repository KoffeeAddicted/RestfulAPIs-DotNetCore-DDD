using Domain;
using Domain.Entities;
using Domain.RepositoryInterfaces;
using Microsoft.EntityFrameworkCore;

namespace Persistence.Repositories;

public class WishlistRepository : IWishlistRepository
{
    private readonly IGenericRepository<Wishlist> _genericRepository;

    public WishlistRepository(IGenericRepository<Wishlist> genericRepository)
    {
        _genericRepository = genericRepository;
    }

    public async Task<IEnumerable<Wishlist>> getStoriesUserWishList(String ProviderToken)
    {
        return await _genericRepository.Table
            .Include(s => s.Story)
            .Where(s => s.ProviderToken  == ProviderToken)
            .ToListAsync();
    }
    public async Task<Wishlist?> getStoryByWishList (String ProviderToken, Int64 StoryId)
    {
        return await _genericRepository.Table
            .Include(s => s.Story)
            .ThenInclude(st => st.Episodes)
            .ThenInclude(e => e.Audio)
            .Where(s => s.ProviderToken == ProviderToken && s.StoryId == StoryId )
            .FirstOrDefaultAsync();
    }

    public void Insert(Wishlist wishlist)
    {
        _genericRepository.Insert(wishlist);
    }

    public void Update(Wishlist wishlist)
    {
        _genericRepository.Update(wishlist);
    }

    public void Delete(Wishlist wishlist)
    {
        _genericRepository.Delete(wishlist);
    }

}