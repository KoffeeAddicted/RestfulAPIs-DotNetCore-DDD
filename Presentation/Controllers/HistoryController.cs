using Contracts;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Services.Absractions;
using System.ComponentModel.DataAnnotations;
using Contracts.DTOs.History;
using System.Collections.Generic;

namespace Presentation.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class HistoryController : ControllerBase
    {
        private readonly IServiceManager _serviceManager;

        public HistoryController(IServiceManager serviceManager)
        {
            _serviceManager = serviceManager;
        }

        [HttpPost]
        [Route("AddStoryUserHistory")]
        [Produces(typeof(ApiResponse<IEnumerable<HistoryResponseDTO>>))]
        public async Task<IActionResult> AddStoryUserHistory([FromBody] AddHistoryStoryRequest request)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            HistoryResponseDTO responseDto = await _serviceManager.HistoryService.AddStoryUserHistory(request);


            ApiResponse<HistoryResponseDTO> response = new ApiResponse<HistoryResponseDTO>()
            {
                Success = true,
                StatusCode = StatusCodes.Status200OK,
                Message = "Success",

            };
            return Ok(response);
        }


        [HttpGet]
        [Route("GetUserHistory")]
        [Produces(typeof(ApiResponse<IEnumerable<HistoryResponseDTO>>))]
        public async Task<IActionResult> GetStories([FromQuery] String ProviderToken)
        {
            IEnumerable<HistoryResponseDTO> userHistories = await _serviceManager.HistoryService.GetUserHistory(ProviderToken);

            ApiResponse<IEnumerable<HistoryResponseDTO>> response = new ApiResponse<IEnumerable<HistoryResponseDTO>>()
            {
                Success = true,
                StatusCode = StatusCodes.Status200OK,
                Message = "Success",
                Data = userHistories
            };

            return Ok(response);
        }


        [HttpDelete]
        [Route("DeleteUserHistories")]
        [Produces(typeof(ApiResponse<IEnumerable<HistoryResponseDTO>>))]
        public async Task<IActionResult> DeleteUserHistories([FromBody] DeleteHistoryStoryRequest request)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            IEnumerable<HistoryResponseDTO> responseDto = await _serviceManager.HistoryService.DeleteUserHistories(request.ProviderToken);


            ApiResponse<HistoryResponseDTO> response = new ApiResponse<HistoryResponseDTO>()
            {
                Success = true,
                StatusCode = StatusCodes.Status200OK,
                Message = "Success",

            };
            return Ok(response);
        }
    }
}
