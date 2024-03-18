using Contracts;
using Contracts.DTOs.Stories;
using Domain.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Services.Absractions;
using Services.DTOs.StoriyCategories;

namespace Presentation.Controllers;

[ApiController]
[Route("api/[controller]")]
public class StoryCategoryController : ControllerBase
{
    private readonly IServiceManager _serviceManager;

    public StoryCategoryController(IServiceManager serviceManager)
    {
        _serviceManager = serviceManager;
    }
    
    [HttpGet]
    [Route(nameof(GetStoryCategories))]
    [Produces(typeof(ApiResponse<IEnumerable<StoryCategoryResponseDTO>>))]
    public async Task<IActionResult> GetStoryCategories()
    {
        IEnumerable<StoryCategoryResponseDTO> result = await _serviceManager.StoryCategoryService.GetAllAsync();
        
        ApiResponse<IEnumerable<StoryCategoryResponseDTO>> response = new ApiResponse<IEnumerable<StoryCategoryResponseDTO>>()
        {
            Success = true,
            StatusCode = StatusCodes.Status200OK,
            Message = "Success",
            Data = result
        };
        
        return Ok(response);
    }
}