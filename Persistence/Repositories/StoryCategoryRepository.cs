using System.Collections;
using Domain.Entities;
using Domain.RepositoryInterfaces;
using Microsoft.EntityFrameworkCore;

namespace Persistence.Repositories;

public class StoryCategoryRepository : IStoryCategoryRepository
{
    private readonly IGenericRepository<StoryCategory> _genericRepository;

    public StoryCategoryRepository(IGenericRepository<StoryCategory> genericRepository)
    {
        _genericRepository = genericRepository;
    }
    
    public async Task<IEnumerable<StoryCategory>> GetAllAsync()
    {
        return await _genericRepository.Table
            .Where(s => !s.IsDeleted)
            .ToListAsync();
    }

    public async Task<IEnumerable<StoryCategory>> GetCategoryByNames(IList<string> name)
        => await _genericRepository.Table.Where(st => name.Contains(st.Name)).ToListAsync();
}