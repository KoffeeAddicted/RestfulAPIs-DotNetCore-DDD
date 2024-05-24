namespace Domain.RepositoryInterfaces;

public interface IRepositoryManager
{
    IStoryRepository StoryRepository { get;  }
    IStoryCategoryRepository StoryCategoryRepository { get; }

    IUserRepository UserRepository { get; }
    IWishlistRepository WishlistRepository { get; }
    IHistoryRepository HistoryRepository { get; }
    ICommentRepository CommentRepository { get; }
    IAudioRepository AudioRepository { get; }
}