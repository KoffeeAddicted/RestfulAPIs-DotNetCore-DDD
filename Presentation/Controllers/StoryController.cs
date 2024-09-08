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
    public async Task<IActionResult> GetAuthorStories([FromQuery][Required] Boolean isBook, [FromQuery][Required] Boolean isStory)
    {
        try
        {
            IEnumerable <StoryResponeAuthorDTO> storyResponse = await _serviceManager.StoryService.GetStoryAuthor(isBook, isStory);

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

    [HttpGet]
    [Route(nameof(GetVoiceStories))]
    [Produces(typeof(ApiResponse<IEnumerable<StoryResponeVoiceDTO>>))]
    public async Task<IActionResult> GetVoiceStories([FromQuery][Required] Boolean isBook, [FromQuery][Required] Boolean isStory)
    {
        try
        {
            IEnumerable<StoryResponeVoiceDTO> storyResponse = await _serviceManager.StoryService.GetVoiceAuthor(isBook, isStory);

            ApiResponse<IEnumerable<StoryResponeVoiceDTO>> response = new ApiResponse<IEnumerable<StoryResponeVoiceDTO>>()
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
    
    [HttpPost("upload-banner")]
    public async Task<IActionResult> UploadImage(IFormFile image)
    {
        if (image == null || image.Length == 0)
            return BadRequest("No image provided.");

        // Validate the file type
        var permittedExtensions = new[] { ".jpg", ".jpeg", ".png", ".gif" };
        var extension = Path.GetExtension(image.FileName).ToLowerInvariant();

        if (string.IsNullOrEmpty(extension) || !permittedExtensions.Contains(extension))
        {
            return BadRequest("Invalid file type. Only .jpg, .jpeg, .png, and .gif files are allowed.");
        }

        // Validate MIME type
        if (!image.ContentType.StartsWith("image/"))
        {
            return BadRequest("Invalid content type. Only image files are allowed.");
        }

        _serviceManager.StoryService.AddBanner(image);
        
        ApiResponse<string> response = new ApiResponse<string>
        {
            Success = true,
            StatusCode = StatusCodes.Status200OK,
            Message = "Success",
            Data = "Upload banner success"
        };

        return Ok(response);
    }
    
    [HttpGet("get-latest-banner")]
    public async Task<IActionResult> GetImage()
    {
        var imageEntity = await _serviceManager.StoryService.GetLatestBanner();
        if (imageEntity == null)
            throw new Exception("Banner not found");

        var imageBytes = Convert.FromBase64String(imageEntity.Content);
        return File(imageBytes, "image/jpeg");
    }
}