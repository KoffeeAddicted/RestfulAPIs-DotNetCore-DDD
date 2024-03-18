using Domain.Entities;

namespace Domain.RepositoryInterfaces;

public interface IStoryRepository
{
    Task<IEnumerable<Story>> GetAllAsync();
    Task<IEnumerable<Story>> GetByFilter(String name, Int64 storyCategoryId, Boolean isBook, Boolean isStory);
    Task<Story> GetByIdAsync(Int64 id);
    void Insert(Story story);
    void Update(Story story);
    void Delete(Story story);
}