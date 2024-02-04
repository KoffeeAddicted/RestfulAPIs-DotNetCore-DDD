using Domain.Entities;

namespace Domain.RepositoryInterfaces;

public interface IStoryRepository
{
    Task<IEnumerable<Story>> GetAllAsync();
    Task<Story> GetByIdAsync(Int64 id);
    void Insert(Story story);
    void Update(Story story);
    void Delete(Story story);
}