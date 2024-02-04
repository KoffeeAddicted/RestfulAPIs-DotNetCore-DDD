using Domain.Entities;
using Domain.RepositoryInterfaces;

namespace Persistence.Repositories;

public sealed class RepositoryManager : IRepositoryManager
{
    private readonly Lazy<IStoryRepository> _lazyStoryRepository;

    public RepositoryManager(IGenericRepository<Story> genericStoryRepository)
    {
        _lazyStoryRepository = new Lazy<IStoryRepository>(() => new StoryRepository(genericStoryRepository));
    }

    public IStoryRepository StoryRepository => _lazyStoryRepository.Value;
}