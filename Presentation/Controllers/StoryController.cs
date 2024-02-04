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
    [Produces(typeof(ApiResponse<IEnumerable<StoryResponseDTO>>))]
    public async Task<IActionResult> GetStories([FromQuery] ListFilter filter)
    {
        StoriesFilteredResponse storyResponse = await _serviceManager.StoryService.GetStoriesAsync(filter);

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
        
        return Ok();
    }
}