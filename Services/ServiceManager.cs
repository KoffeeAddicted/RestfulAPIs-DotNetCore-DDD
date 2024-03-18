using AutoMapper;
using Contracts;
using Domain.RepositoryInterfaces;
using Microsoft.Extensions.Options;
using Services.Absractions;

namespace Services;

public sealed class ServiceManager : IServiceManager
{
    private readonly Lazy<IStoryService> _lazyStoryService;
    private readonly Lazy<IStoryCategoryService> _lazyStoryCategoryService;

    public ServiceManager(IRepositoryManager repositoryManager, IMapper mapper, IOptions<AppSettings> appSettings)
    {
        _lazyStoryService = new Lazy<IStoryService>(() => new StoryService(repositoryManager, mapper, appSettings));
        _lazyStoryCategoryService = new Lazy<IStoryCategoryService>(() => new StoryCategoryService(repositoryManager, mapper));
    }

    public IStoryService StoryService => _lazyStoryService.Value;
    public IStoryCategoryService StoryCategoryService => _lazyStoryCategoryService.Value;
}