using Domain.Entities;

namespace Domain.RepositoryInterfaces;

public interface IStoryCategoryRepository
{
    Task<IEnumerable<StoryCategory>> GetAllAsync();
    Task<IEnumerable<StoryCategory>> GetCategoryByNames(IList<string> name);
    Task<IEnumerable<StoryCategory>> GetCategoryByIds(IList<long> ids);
}