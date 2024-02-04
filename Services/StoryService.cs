using AutoMapper;
using Contracts;
using Contracts.DTOs.Stories;
using Domain.Entities;
using Domain.RepositoryInterfaces;
using Services.Absractions;
using Services.DbEnum;

namespace Services;

public class StoryService : IStoryService
{
    private readonly IRepositoryManager _repositoryManager;
    private readonly IMapper _mapper;

    public StoryService(IRepositoryManager repositoryManager,
        IMapper mapper
        )
    {
        _repositoryManager = repositoryManager;
        _mapper = mapper;
    }

    public async Task<StoriesFilteredResponse> GetStoriesAsync(ListFilter filter)
    {
        IEnumerable<Story> stories = await _repositoryManager.StoryRepository.GetAllAsync();

        stories = stories.Where(s => s.Name.Contains(filter.SearchName));

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
}