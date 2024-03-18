using System.Collections;
using AutoMapper;
using Contracts;
using Contracts.DTOs.Stories;
using Contracts.Helpers;
using Domain;
using Domain.Entities;
using Domain.RepositoryInterfaces;
using Microsoft.Extensions.Options;
using Services.Absractions;
using Services.DbEnum;
using Services.DTOs.Episodes;

namespace Services;

public class StoryService : IStoryService
{
    private readonly IRepositoryManager _repositoryManager;
    private readonly IMapper _mapper;
    private readonly AppSettings _appSettings;

    public StoryService(IRepositoryManager repositoryManager,
        IMapper mapper,
        IOptions<AppSettings> appSettings)
    {
        _repositoryManager = repositoryManager;
        _mapper = mapper;
        _appSettings = appSettings.Value;
    }

    public async Task<StoriesFilteredResponse> GetStoriesAsync(ListFilter filter, Int64 storyCategoryId, Boolean isStory, Boolean isBook)
    {
        IEnumerable<Story> stories = await _repositoryManager.StoryRepository.GetByFilter(filter.SearchName, storyCategoryId, isBook, isStory);

        Int32 totalCount = stories.Count();

        switch (filter.OrderType)
        {
            case OrderTypeEnum.Ascending:
                stories = stories.OrderBy(s => s.Id);
                break;
            case OrderTypeEnum.Descending:
                stories = stories.OrderByDescending(s => s.Id);
                break;
            default:
                stories = stories.OrderByDescending(s => s.Id);
                break;
        }
        
        stories = stories.Skip((filter.Page - 1) * filter.PageSize).Take(filter.PageSize);

        IEnumerable<StoryResponseDTO> storiesResponse =
            _mapper.Map<IEnumerable<Story>, IEnumerable<StoryResponseDTO>>(stories);

        StoriesFilteredResponse response = new StoriesFilteredResponse()
        {
            StoryResponseDtos = storiesResponse,
            Page = filter.Page,
            PageSize = filter.PageSize,
            TotalCount = totalCount
        };

        return response;
    }

    public async Task<StoryResponseDTO> CreateStoryAsync(StoryCreateRequest storyCreateRequest)
    {
        if(string.IsNullOrEmpty(storyCreateRequest.Thumbnail))
            storyCreateRequest.Thumbnail = await YoutubeHelper.GetThumbnail(storyCreateRequest.Episodes.FirstOrDefault().File);
        
        foreach (EpisodeCreateRequest episode in storyCreateRequest.Episodes)
        {
            switch (episode.InputAudioType)
            {
                case InputUploadAudioTypeEnum.YoutubeLink:
                    String link = await UploadYoutubeVideoToS3(episode.File);
                    episode.Duration = await YoutubeHelper.GetVideoDuration();
                    episode.File = link;
                    break;
                default:
                    break;
            }
        }
        
        Story story = _mapper.Map<StoryCreateRequest, Story>(storyCreateRequest);
        
        _repositoryManager.StoryRepository.Insert(story);

        StoryResponseDTO response = _mapper.Map<Story, StoryResponseDTO>(story);

        return response;
    }

    public async Task<String> UploadYoutubeVideoToS3(string url)
    {
        YoutubeHelper.url = url;
        
        Stream videoStream = await YoutubeHelper.DownloadYoutubeVideo();
        
        String link = await ConvertToAudioHelper.ConvertStreamVideoToAudioStreamAndUploadOnS3(_appSettings, videoStream);

        return $"{_appSettings.AwsSettings.CloudFrontDomain}/{link}";
    }
}