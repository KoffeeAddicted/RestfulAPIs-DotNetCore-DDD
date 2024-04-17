using Domain;
using Domain.Entities;
using Domain.RepositoryInterfaces;
using Microsoft.EntityFrameworkCore;

namespace Persistence.Repositories;

public class HistoryRepository : IHistoryRepository
{
    private readonly IGenericRepository<History> _genericRepository;

    public HistoryRepository(IGenericRepository<History> genericRepository)
    {
        _genericRepository = genericRepository;
    }

    public async Task<IEnumerable<History>> getStoriesUserHistory(String ProviderToken)
    {
        return await _genericRepository.Table
            .Include(s => s.Story)
            .Where(s => s.ProviderToken == ProviderToken)
            .ToListAsync();
    }

    public void Insert(History history)
    {
        _genericRepository.Insert(history);
    }

    public void Update(History history)
    {
        _genericRepository.Update(history);
    }

    public void Delete(IEnumerable<History> histories)
    {
        _genericRepository.Delete(histories);
    }

}