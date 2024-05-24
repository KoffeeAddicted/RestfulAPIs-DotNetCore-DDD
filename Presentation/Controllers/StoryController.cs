using System.ComponentModel.DataAnnotations;
using Contracts;
using Contracts.DTOs.Stories;
using Domain.Exceptions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OfficeOpenXml;
using Services.Absractions;

namespace Presentation.Controllers;

[ApiController]
[Route("api/[controller]")]
public class StoryController : ControllerBase
{
    private readonly IServiceManager _serviceManager;

    public StoryController(IServiceManager serviceManager)
    {
        _serviceManager = serviceManager;
    }
    
    [HttpGet]
    [Route(nameof(GetStories))]
    [Produces(typeof(ApiResponse<IEnumerable<StoriesFilteredResponse>>))]
    public async Task<IActionResult> GetStories([FromQuery] ListFilter filter, [FromQuery][Required] Int64 storyCategoryId, [FromQuery][Required] Boolean isBook, [FromQuery][Required] Boolean isStory)
    {
        StoriesFilteredResponse storyResponse = await _serviceManager.StoryService.GetStoriesAsync(filter, storyCategoryId, isStory, isBook);

        ApiResponse<StoriesFilteredResponse> response = new ApiResponse<StoriesFilteredResponse>()
        {
            Success = true,
            StatusCode = StatusCodes.Status200OK,
            Message = "Success",
            Data = storyResponse
        };
        
        return Ok(response);
    }
    
    [HttpPost]
    [Route(nameof(CreateNewStory))]
    [Produces(typeof(ApiResponse<IEnumerable<StoryResponseDTO>>))]
    public async Task<IActionResult> CreateNewStory([FromBody] StoryCreateRequest request)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        StoryResponseDTO responseDto = await _serviceManager.StoryService.CreateStoryAsync(request);
        
        return Ok(responseDto);
    }
    
    [HttpPost]
    [Route(nameof(UploadStoriesFromExcel))]
    [Produces(typeof(ApiResponse<IEnumerable<StoryResponseDTO>>))]
    public async Task<IActionResult> UploadStoriesFromExcel(IFormFile excelFile)
    {
        if (excelFile == null || excelFile.Length == 0)
            return BadRequest("No file uploaded or file is empty.");

        List<StoryResponseDTO> stories = new List<StoryResponseDTO>();

        using (var stream = new MemoryStream())
        {
            await excelFile.CopyToAsync(stream);
            using (var package = new ExcelPackage(stream))
            {
                ExcelWorksheet worksheet = package.Workbook.Worksheets[0];
                int rowCount = worksheet.Dimension.Rows;
                int colCount = worksheet.Dimension.Columns;

                // for (int row = 2; row <= rowCount; row++) // Assuming first row is headers
                // {
                //     var storyRequest = new StoryCreateRequest
                //     {
                //         Title = worksheet.Cells[row, 1].Text,
                //         Content = worksheet.Cells[row, 2].Text,
                //         CategoryId = long.Parse(worksheet.Cells[row, 3].Text),
                //         // Add other properties as necessary
                //     };
                //
                //     var responseDto = await _serviceManager.StoryService.CreateStoryAsync(storyRequest);
                //     stories.Add(responseDto);
                // }
            }
        }

        ApiResponse<IEnumerable<StoryResponseDTO>> response = new ApiResponse<IEnumerable<StoryResponseDTO>>
        {
            Success = true,
            StatusCode = StatusCodes.Status200OK,
            Message = "Stories successfully uploaded and processed.",
            Data = stories
        };

        return Ok(response);
    }
}