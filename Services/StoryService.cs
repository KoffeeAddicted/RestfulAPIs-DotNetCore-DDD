using System.Collections;
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
        
        Story story = _mapper.Map<StoryCreateRequest, Story>(storyCreateRequest);
        
        _repositoryManager.StoryRepository.Insert(story);

        StoryResponseDTO response = _mapper.Map<Story, StoryResponseDTO>(story);

        return response;
    }

    public async Task<List<StoryCreateRequest>> GetStoryRequestListByExcel(IFormFile excelFile)
    {
        // StoryCreateRequest storyCreateRequests = new StoryCreateRequest();
        //
        // using (var stream = new MemoryStream())
        // {
        //     await excelFile.CopyToAsync(stream);
        //     using (var package = new ExcelPackage(stream))
        //     {
        //         ExcelWorksheet worksheet = package.Workbook.Worksheets[0];
        //         int rowCount = worksheet.Dimension.Rows;
        //         int colCount = worksheet.Dimension.Columns;
        //
        //         // Validate Excel template
        //         if (!ValidateExcelTemplate(worksheet))
        //         {
        //             throw new Exception("Invalid Excel template.");
        //         }
        //
        //         for (int row = 2; row <= rowCount; row++) // Assuming first row is headers
        //         {
        //             var storyRequest = new StoryCreateRequest
        //             {
        //                 Name = worksheet.Cells[row, 1].Text,
        //                 Author = worksheet.Cells[row, 2].Text,
        //                 Voice = worksheet.Cells[row, 3].Text,
        //                 Description = worksheet.Cells[row, 4].Text,
        //                 Type = worksheet.Cells[row, 5].Text,
        //                 Category = worksheet.Cells[row, 6].Text,
        //                 Link = worksheet.Cells[row, 7].Text,
        //                 Source = worksheet.Cells[row, 8].Text,
        //                 Episode = int.Parse(worksheet.Cells[row, 9].Text)
        //             };
        //
        //             storyCreateRequests.Add(storyRequest);
        //         }
        //     }
        // }
        //
        // return storyCreateRequests;

        return null;
    }
    
    private bool ValidateExcelTemplate(ExcelWorksheet worksheet)
    {
        var headers = new List<string> { "Tên", "Tác giả", "Giọng đọc", "Mô tả", "Sách/Truyện", "Thể loại", "Link", "Nguồn", "Tập" };

        for (int col = 1; col <= headers.Count; col++)
        {
            if (worksheet.Cells[1, col].Text != headers[col - 1])
            {
                return false;
            }
        }
        return true;
    }
}