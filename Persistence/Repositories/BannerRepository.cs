using Domain.Entities;
using Domain.RepositoryInterfaces;
using LinqToDB.Tools;
using Microsoft.EntityFrameworkCore;

namespace Persistence.Repositories;

public class BannerRepository : IBannerRepository
{
    private readonly IGenericRepository<Banner> _genericRepository;

    public BannerRepository(IGenericRepository<Banner> genericRepository)
    {
        _genericRepository = genericRepository;
    }

    public async Task<Banner?> GetLatest()
    {
        return await _genericRepository.Table
            .Where(p => !p.IsDeleted)
            .OrderByDescending(p => p.CreatedDateTime)
            .FirstOrDefaultAsync();
    }

    public void Add(Banner banner)
    {
        _genericRepository.Insert(banner);
    }
}