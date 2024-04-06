using Domain;
using Domain.Entities;
using Domain.RepositoryInterfaces;

namespace Persistence.Repositories;

public sealed class RepositoryManager : IRepositoryManager
{
    private readonly Lazy<IStoryRepository> _lazyStoryRepository;
    private readonly Lazy<IStoryCategoryRepository> _lazyStoryCategoryRepository;
    private readonly Lazy<IUserRepository> _lazyUserRepository;
    public RepositoryManager(IGenericRepository<Story> genericStoryRepository,
        IGenericRepository<StoryCategory> genericStoryCategoryRepository, IGenericRepository<User>
        genericUserRepository)
    {
        _lazyStoryRepository = new Lazy<IStoryRepository>(() => new StoryRepository(genericStoryRepository));
        _lazyStoryCategoryRepository = new Lazy<IStoryCategoryRepository>(() => new StoryCategoryRepository(genericStoryCategoryRepository));
        _lazyUserRepository = new Lazy<IUserRepository>(() => new UserRepository(genericUserRepository));
    }

    public IStoryRepository StoryRepository => _lazyStoryRepository.Value;
    public IUserRepository UserRepository => _lazyUserRepository.Value;
    public IStoryCategoryRepository StoryCategoryRepository => _lazyStoryCategoryRepository.Value;
}