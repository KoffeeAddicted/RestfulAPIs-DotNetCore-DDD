using System.ComponentModel.DataAnnotations;
using Contracts;
using Contracts.DTOs.Stories;
using Contracts.DTOs.Users;
using Domain.Exceptions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Services.Absractions;

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

        return Ok(responseDto);
    }
}