using AutoMapper;
using Domain.RepositoryInterfaces;
using Services.Absractions;

namespace Services;

public sealed class ServiceManager : IServiceManager
{
    private readonly Lazy<IStoryService> _lazyStoryService;

    public ServiceManager(IRepositoryManager repositoryManager, IMapper mapper)
    {
        _lazyStoryService = new Lazy<IStoryService>(() => new StoryService(repositoryManager, mapper));
    }

    public IStoryService StoryService => _lazyStoryService.Value;
}