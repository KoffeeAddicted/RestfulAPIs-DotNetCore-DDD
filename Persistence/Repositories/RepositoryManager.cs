using Domain.Entities;
using Domain.RepositoryInterfaces;

namespace Persistence.Repositories;

public sealed class RepositoryManager : IRepositoryManager
{
    private readonly Lazy<IStoryRepository> _lazyStoryRepository;
    private readonly Lazy<IStoryCategoryRepository> _lazyStoryCategoryRepository;

    public RepositoryManager(IGenericRepository<Story> genericStoryRepository,
        IGenericRepository<StoryCategory> genericStoryCategoryRepository)
    {
        _lazyStoryRepository = new Lazy<IStoryRepository>(() => new StoryRepository(genericStoryRepository));
        _lazyStoryCategoryRepository = new Lazy<IStoryCategoryRepository>(() => new StoryCategoryRepository(genericStoryCategoryRepository));
    }

    public IStoryRepository StoryRepository => _lazyStoryRepository.Value;
    public IStoryCategoryRepository StoryCategoryRepository => _lazyStoryCategoryRepository.Value;
}