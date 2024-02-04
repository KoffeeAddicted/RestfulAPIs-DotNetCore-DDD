namespace Domain.RepositoryInterfaces;

public interface IRepositoryManager
{
    IStoryRepository StoryRepository { get;  }
}