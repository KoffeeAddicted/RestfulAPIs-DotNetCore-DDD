namespace Services.Absractions;

public interface IServiceManager
{ 
    IStoryService StoryService { get; }
    IStoryCategoryService StoryCategoryService { get; }
    IUserService UserService { get; }
}