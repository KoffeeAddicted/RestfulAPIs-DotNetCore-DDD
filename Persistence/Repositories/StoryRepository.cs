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
    
    public async Task<IEnumerable<Story>> GetByFilter(String name, IList<long> storyCategoryId, Boolean isBook, Boolean isStory)
    {
        IQueryable<Story> query = _genericRepository.Table
            .Include(s => s.Episodes.Where(e => !e.IsDeleted))
            .ThenInclude(e => e.Audio)
            .Where(s => !s.IsDeleted
                        && s.IsBook == isBook
                        && s.IsStory == isStory);

        // Apply case-insensitive search filter only if searchName is not null or empty
        if (!string.IsNullOrEmpty(name))
        {
            // Convert searchName to lowercase for case-insensitive comparison
            string lowercaseName = name.ToLower();
            query = query.Where(s => s.Name.ToLower().Contains(lowercaseName)
                                     || s.Description.ToLower().Contains(lowercaseName)
                                     || s.Voice.ToLower().Contains(lowercaseName)
                                     || s.Author.ToLower().Contains(lowercaseName));
        }

        if (storyCategoryId.Any())
            query = query.Where(s => s.StoryCategoryId.Any(sc => storyCategoryId.Contains(sc)));

        return await query.ToListAsync();
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
    
    public async Task Insert(List<Story> stories)
    {
        foreach (var story in stories)
        {
            await _genericRepository.InsertAsync(story);
        }
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