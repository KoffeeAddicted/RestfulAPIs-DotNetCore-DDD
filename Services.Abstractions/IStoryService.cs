using Contracts;
using Contracts.DTOs.Stories;

namespace Services.Absractions;

public interface IStoryService
{
    Task<StoriesFilteredResponse> GetStoriesAsync(ListFilter filter);
    // Task<StoryResponseDTO> CreateStoryAsync();
}