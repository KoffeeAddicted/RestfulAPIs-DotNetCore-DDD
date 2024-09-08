using Contracts;
using Contracts.DTOs.Stories;
using Domain.Entities;
using Microsoft.AspNetCore.Http;

namespace Services.Absractions;

public interface IStoryService
{
    Task<StoriesFilteredResponse> GetStoriesAsync(ListFilter filter, IList<long> storyCategoryId, Boolean isStory, Boolean isBook);

    Task<IEnumerable<StoryResponeAuthorDTO>> GetStoryAuthor(Boolean isBook, Boolean isStory);

    Task<IEnumerable<StoryResponeVoiceDTO>> GetVoiceAuthor(Boolean isBook, Boolean isStory);

    Task<StoryResponseDTO> CreateStoryAsync(StoryCreateRequest storyCreateRequest);
    Task<IEnumerable<StoryResponseDTO>> UploadStoriesByExcel(IFormFile excelFile);
    Task<Banner?> GetLatestBanner();
    void AddBanner(IFormFile image);
}