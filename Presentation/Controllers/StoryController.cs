using System.ComponentModel.DataAnnotations;
using Contracts;
using Contracts.DTOs.Stories;
using Domain.Exceptions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
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
}