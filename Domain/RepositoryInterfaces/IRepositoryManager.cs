namespace Domain.RepositoryInterfaces;

public interface IRepositoryManager
{
    IStoryRepository StoryRepository { get;  }
    IStoryCategoryRepository StoryCategoryRepository { get; }

    IUserRepository UserRepository { get; }
}