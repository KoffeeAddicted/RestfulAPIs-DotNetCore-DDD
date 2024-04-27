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
    private readonly Lazy<IUserService> _lazyUserService;
    private readonly Lazy<IWishlistService> _lazyWishlistService;
    private readonly Lazy<IHistoryService> _lazyHistoryService;
    private readonly Lazy<ICommentService> _lazyCommentService;

    public ServiceManager(IRepositoryManager repositoryManager, IMapper mapper, IOptions<AppSettings> appSettings)
    {
        _lazyStoryService = new Lazy<IStoryService>(() => new StoryService(repositoryManager, mapper, appSettings));
        _lazyStoryCategoryService = new Lazy<IStoryCategoryService>(() => new StoryCategoryService(repositoryManager, mapper));
        _lazyUserService = new Lazy<IUserService>(() => new UserService(repositoryManager, mapper, appSettings));
        _lazyWishlistService = new Lazy<IWishlistService>(() => new WishlistService(repositoryManager, mapper, appSettings));
        _lazyHistoryService = new Lazy<IHistoryService>(() => new HistoryService(repositoryManager, mapper, appSettings));
        _lazyCommentService = new Lazy<ICommentService>(() => new CommentService(repositoryManager, mapper,appSettings));
    }

    public IStoryService StoryService => _lazyStoryService.Value;
    public IStoryCategoryService StoryCategoryService => _lazyStoryCategoryService.Value;
    public IUserService UserService => _lazyUserService.Value;
    public IWishlistService WishlistService => _lazyWishlistService.Value;
    public IHistoryService HistoryService => _lazyHistoryService.Value;
    public ICommentService CommentService => _lazyCommentService.Value;
}