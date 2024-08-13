using Domain.Entities;

namespace Domain.RepositoryInterfaces;

public interface IStoryRepository
{
    Task<IEnumerable<Story>> GetAllAsync();

    Task<IEnumerable<Story>> getAuthorAudioVideoAsync();
    Task<IEnumerable<Story>> GetByFilter(String name, IList<long> storyCategoryId, Boolean isBook, Boolean isStory);
    Task<Story> GetByIdAsync(Int64 id);
    void Insert(Story story);
    Task Insert(List<Story> stories);
    void Update(Story story);
    void Delete(Story story);
}