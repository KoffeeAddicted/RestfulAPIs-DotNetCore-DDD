using Contracts;
using Contracts.Helpers;
using Domain.Entities;
using Domain.RepositoryInterfaces;
using Microsoft.EntityFrameworkCore;


namespace Persistence.Repositories;

public class StoryRepository : IStoryRepository
{
    private readonly IGenericRepository<Story> _genericRepository;

    public StoryRepository(IGenericRepository<Story> genericRepository)
    {
        _genericRepository = genericRepository;
    }
    
    public async Task<IEnumerable<Story>> GetAllAsync()
    {
        return await _genericRepository.Table
            .Include(s => s.Episodes.Where(e => !e.IsDeleted))
            .ThenInclude(e => e.Audio)
            .Where(s => !s.IsDeleted)
            .ToListAsync();
    }

    public async Task<Story> GetByIdAsync(Int64 id)
    {
        return await _genericRepository.Table
            .FirstOrDefaultAsync(s => !s.IsDeleted && s.Id == id);
    }

    public void Insert(Story story)
    {
        _genericRepository.Insert(story);
    }

    public void Update(Story story)
    {
        _genericRepository.Update(story);
    }

    public void Delete(Story story)
    {
        _genericRepository.Delete(story);
    }
}