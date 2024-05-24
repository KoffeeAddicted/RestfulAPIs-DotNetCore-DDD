using Domain;
using Domain.RepositoryInterfaces;
using Microsoft.EntityFrameworkCore;

namespace Persistence.Repositories;

public class AudioRepository : IAudioRepository
{
    private readonly IGenericRepository<Audio> _genericRepository;

    public AudioRepository(IGenericRepository<Audio> genericRepository)
    {
        _genericRepository = genericRepository;
    }

    public async Task<IEnumerable<Audio>> GetTop10YoutubeVideoAsync()
    {
        return await _genericRepository.Table
            .Where(a => a.Link.Contains("www.youtube.com"))
            .OrderByDescending(a => a.Id)
            .Take(10).ToListAsync();
    }

    public void UpdateAsync(List<Audio> audios)
        => _genericRepository.Update(audios);
}