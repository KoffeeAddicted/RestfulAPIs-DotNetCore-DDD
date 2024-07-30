using System.Collections;
using Amazon.Runtime.Internal.Transform;
using AutoMapper;
using Contracts;
using Contracts.DTOs.Stories;
using Contracts.Helpers;
using Domain;
using Domain.Entities;
using Domain.RepositoryInterfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using OfficeOpenXml;
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

    public async Task<StoriesFilteredResponse> GetStoriesAsync(ListFilter filter, IList<long> storyCategoryId, Boolean isStory, Boolean isBook)
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
        
        Story story = _mapper.Map<StoryCreateRequest, Story>(storyCreateRequest);
        
        _repositoryManager.StoryRepository.Insert(story);

        StoryResponseDTO response = _mapper.Map<Story, StoryResponseDTO>(story);

        return response;
    }

    public async Task<IEnumerable<StoryResponseDTO>> UploadStoriesByExcel(IFormFile excelFile)
    {
        try
        {
            IEnumerable<StoryResponseDTO> storiesDto = new List<StoryResponseDTO>();
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;

            if (excelFile == null || excelFile.Length == 0)
                throw new Exception("No file uploaded or file is empty.");

            bool validationErrors = ValidateExcelFile(excelFile);

            if (validationErrors)
            {
                throw new Exception("Template not true");
            }

            List<StoryCreateRequest> storiesRequest = await ReadStoriesFromExcel(excelFile);

            if (storiesRequest.Any())
            {
                await Task.WhenAll(storiesRequest
                    .Where(story => string.IsNullOrEmpty(story.Thumbnail))
                    .Select(async story => story.Thumbnail = await YoutubeHelper.GetThumbnail(story.Episodes.FirstOrDefault()?.File)));
                
                List<Story> stories = _mapper.Map<List<StoryCreateRequest>, List<Story>>(storiesRequest);

                await _repositoryManager.StoryRepository.Insert(stories);
                
                storiesDto = _mapper.Map<IEnumerable<Story>, IEnumerable<StoryResponseDTO>>(stories);
            }

            return storiesDto;
        }
        catch (Exception e)
        {
            throw e;
        }
    }
    
    private async Task<List<StoryCreateRequest>> ReadStoriesFromExcel(IFormFile excelFile)
    {
        var stories = new List<StoryCreateRequest>();
        StoryCreateRequest currentStory = null;

        using (var stream = new MemoryStream())
        {
            await excelFile.CopyToAsync(stream);
            using (var package = new ExcelPackage(stream))
            {
                ExcelWorksheet worksheet = package.Workbook.Worksheets[0];
                int rowCount = worksheet.Dimension.Rows;

                for (int row = 2; row <= rowCount; row++) // Assuming first row is headers
                {
                    var titleCell = worksheet.Cells[row, 1].Text;

                    if (titleCell.Equals("End", StringComparison.OrdinalIgnoreCase))
                        break;
                    
                    if (!string.IsNullOrEmpty(titleCell))
                    {
                        currentStory = new StoryCreateRequest
                        {
                            Name = worksheet.Cells[row, 2].Text,
                            Author = worksheet.Cells[row, 3].Text,
                            Voice = worksheet.Cells[row, 4].Text,
                            Desription = worksheet.Cells[row, 5].Text,
                            IsBook = worksheet.Cells[row, 6].Text.Equals("Sách", StringComparison.OrdinalIgnoreCase),
                            IsStory = worksheet.Cells[row, 6].Text.Equals("Truyện", StringComparison.OrdinalIgnoreCase),
                            StoryCategoryId = await StoryCategoryParse(worksheet.Cells[row, 7].Text),
                            SourceDescription = worksheet.Cells[row, 9].Text,
                            Thumbnail = worksheet.Cells[row, 11].Text,
                            Episodes = new List<EpisodeCreateRequest>(),
                            CreatedByName = "Excel",
                            CreatedTime = DateTime.UtcNow,
                            CreatedById = 0
                        };
                        
                        stories.Add(currentStory);
                    }
                    if (currentStory != null)
                    {
                        var episode = new EpisodeCreateRequest
                        {
                            OrderNumber = int.Parse(worksheet.Cells[row, 10].Text),
                            File = worksheet.Cells[row, 8].Text,
                            InputAudioType = DetermineInputAudioType(worksheet.Cells[row, 8].Text)
                        };

                        currentStory.Episodes.Add(episode);
                    }
                }
            }
        }
        return stories;
    }
    
    private bool ValidateExcelFile(IFormFile excelFile)
    {
        var expectedColumns = new List<string>
        {
            "Tên", "Mô tả", "Thể loại", "Tác giả", "Giọng đọc", "Link", "Nguồn", "Tập", "Trang bìa", "STT"
        };

        using (var stream = new MemoryStream())
        {
            excelFile.CopyTo(stream);
            using (var package = new ExcelPackage(stream))
            {
                ExcelWorksheet worksheet = package.Workbook.Worksheets[0];
                int colCount = worksheet.Dimension.Columns;

                if (colCount == expectedColumns.Count() + 1)
                    return false;
            }
        }

        return true;
    }

    private async Task<List<long>> StoryCategoryParse(string name)
    {
        List<string> listname = name.Split(", ").ToList();

        IEnumerable<StoryCategory> storyCategories =
            await _repositoryManager.StoryCategoryRepository.GetCategoryByNames(listname);

        return storyCategories.Select(s => s.Id).ToList();
    }
    
    
    private bool IsUriAndEndsWithExtension(string url, params string[] extensions)
    {
        if (!Uri.TryCreate(url, UriKind.Absolute, out var uri))
            return false;

        var path = uri.LocalPath;
        foreach (var extension in extensions)
        {
            if (path.EndsWith(extension, StringComparison.OrdinalIgnoreCase))
                return true;
        }

        return false;
    }
    
    private InputUploadAudioTypeEnum DetermineInputAudioType(string link)
    {
        if (IsYouTubeLink(link))
        {
            return InputUploadAudioTypeEnum.YoutubeLink;
        }
        if (IsUriAndEndsWithExtension(link, ".mp3", ".wav", ".ogg"))
        {
            return InputUploadAudioTypeEnum.AudioLink;
        }
        if (IsUriAndEndsWithExtension(link, ".mp4", ".avi", ".mkv"))
        {
            return InputUploadAudioTypeEnum.VideoLink;
        }

        throw new Exception("Invalid file type or format.");
    }
    
    private bool IsYouTubeLink(string url)
    {
        // Example: https://www.youtube.com/watch?v=dQw4w9WgXcQ
        var uri = new Uri(url);

        if (uri.Host.ToLowerInvariant() != "www.youtube.com" && uri.Host.ToLowerInvariant() != "youtu.be")
            return false;

        return true;
    }
}