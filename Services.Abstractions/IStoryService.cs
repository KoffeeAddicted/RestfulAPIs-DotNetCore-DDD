using Contracts;
using Contracts.DTOs.Stories;

namespace Services.Absractions;

public interface IStoryService
{
    Task<StoriesFilteredResponse> GetStoriesAsync(ListFilter filter, Int64 storyCategoryId, Boolean isStory, Boolean isBook);
    Task<StoryResponseDTO> CreateStoryAsync(StoryCreateRequest storyCreateRequest);
}