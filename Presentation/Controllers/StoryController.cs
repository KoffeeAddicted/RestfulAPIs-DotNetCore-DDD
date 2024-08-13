using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Contracts;
using Contracts.DTOs.Stories;
using Domain.Exceptions;
using Microsoft.AspNetCore.Authorization;
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
    public async Task<IActionResult> GetStories([FromQuery] ListFilter filter, [FromQuery][Required] IList<long> storyCategoryId, [FromQuery][Required] Boolean isBook, [FromQuery][Required] Boolean isStory)
    {
        try
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
        catch (Exception e)
        {
            throw e;
        }
    }

    [HttpGet]
    [Route(nameof(GetAuthorStories))]
    [Produces(typeof(ApiResponse<IEnumerable<StoryResponeAuthorDTO>>))]
    public async Task<IActionResult> GetAuthorStories()
    {
        try
        {
            IEnumerable <StoryResponeAuthorDTO> storyResponse = await _serviceManager.StoryService.GetStoryAuthor();

            ApiResponse<IEnumerable<StoryResponeAuthorDTO>> response = new ApiResponse<IEnumerable<StoryResponeAuthorDTO>>()
            {
                Success = true,
                StatusCode = StatusCodes.Status200OK,
                Message = "Success",
                Data = storyResponse
            };

            return Ok(response);
        }
        catch (Exception e)
        {
            throw e;
        }
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
        try
        {

            IEnumerable<StoryResponseDTO> stories = await _serviceManager.StoryService.UploadStoriesByExcel(excelFile);
        
            ApiResponse<IEnumerable<StoryResponseDTO>> response = new ApiResponse<IEnumerable<StoryResponseDTO>>
            {
                Success = true,
                StatusCode = StatusCodes.Status200OK,
                Message = "Stories successfully uploaded and processed.",
                Data = stories
            };

            return Ok(response);
        }
        catch (Exception e)
        {
            throw e;
        }
    }
}