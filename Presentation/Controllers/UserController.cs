using System.ComponentModel.DataAnnotations;
using Contracts;
using Contracts.DTOs.Stories;
using Contracts.DTOs.Users;
using Domain.Exceptions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Services.Absractions;
using Services.DTOs.StoriyCategories;

namespace Presentation.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UserController : ControllerBase
{
    private readonly IServiceManager _serviceManager;

    public UserController(IServiceManager serviceManager)
    {
        _serviceManager = serviceManager;
    }

    [HttpPost]
    [Route("CreateAuthen")]
    [Produces(typeof(ApiResponse<IEnumerable<UserResponseDTO>>))]
    public async Task<IActionResult> CreateNewAuthenToken([FromBody] UserCreateCustomerAuthen request)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        UserResponseDTO responseDto = await _serviceManager.UserService.CreateUserAsync(request);


        ApiResponse<UserResponseDTO> response = new ApiResponse<UserResponseDTO>()
        {
            Success = true,
            StatusCode = StatusCodes.Status200OK,
            Message = "Success",
            Data = responseDto
        };
        return Ok(response);
    }


    [HttpGet]
    [Route("GetUser")]
    [Produces(typeof(ApiResponse<UserResponseDTO?>))]
    public async Task<IActionResult> GetUser([FromQuery] String ProviderToken)


        
    {
        UserResponseDTO? userResponse = await _serviceManager.UserService.GetUserAsync(ProviderToken);


        ApiResponse<UserResponseDTO?> response = new()
        {
            Success = true,
            StatusCode = StatusCodes.Status200OK,
            Message = "Success",
            Data = userResponse
        };
        return Ok(response);
    }

    [HttpPost]
    [Route("Login")]
    [Produces(typeof(ApiResponse<UserResponseDTO?>))]
    public async Task<IActionResult> Login([FromBody]  UserLoginRequest request)
    {
        string token = await _serviceManager.UserService.Login(request);
        
        ApiResponse<string> response = new()
        {
            Success = true,
            StatusCode = StatusCodes.Status200OK,
            Message = "Success",
            Data = token
        };
        return Ok(response);
    }
}