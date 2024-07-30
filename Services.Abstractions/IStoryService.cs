using Contracts;
using Contracts.DTOs.Stories;
using Microsoft.AspNetCore.Http;

namespace Services.Absractions;

public interface IStoryService
{
    Task<StoriesFilteredResponse> GetStoriesAsync(ListFilter filter, IList<long> storyCategoryId, Boolean isStory, Boolean isBook);
    Task<StoryResponseDTO> CreateStoryAsync(StoryCreateRequest storyCreateRequest);
    Task<IEnumerable<StoryResponseDTO>> UploadStoriesByExcel(IFormFile excelFile);
}