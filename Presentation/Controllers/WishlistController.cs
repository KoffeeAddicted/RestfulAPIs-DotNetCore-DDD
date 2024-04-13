using Contracts;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Services.Absractions;
using Contracts.DTOs.Wishlist;
using Contracts.DTOs.Stories;
using System.ComponentModel.DataAnnotations;

namespace Presentation.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class WishlistController  : ControllerBase
    {
        private readonly IServiceManager _serviceManager;

        public WishlistController(IServiceManager serviceManager)
        {
            _serviceManager = serviceManager;
        }

        [HttpPost]
        [Route("AddStoryUserWishList")]
        [Produces(typeof(ApiResponse<IEnumerable<WishlistResponseDTO>>))]
        public async Task<IActionResult> AddStoryUserWishList([FromBody] StoryUserWishlistCreate request)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            WishlistResponseDTO responseDto = await _serviceManager.WishlistService.AddStoryUserWishList(request);


            ApiResponse<WishlistResponseDTO> response = new ApiResponse<WishlistResponseDTO>()
            {
                Success = true,
                StatusCode = StatusCodes.Status200OK,
                Message = "Success",
           
            };
            return Ok(response);
        }


        [HttpGet]
        [Route("GetUserWishList")]
        [Produces(typeof(ApiResponse<IEnumerable<WishlistResponseDTO>>))]
        public async Task<IActionResult> GetStories([FromQuery] String ProviderToken)
        {
            IEnumerable<WishlistResponseDTO> userWishList = await _serviceManager.WishlistService.GetUserWishList(ProviderToken);

            ApiResponse<IEnumerable<WishlistResponseDTO>> response = new ApiResponse<IEnumerable<WishlistResponseDTO>>()
            {
                Success = true,
                StatusCode = StatusCodes.Status200OK,
                Message = "Success",
                Data = userWishList
            };

            return Ok(response);
        }

        [HttpGet]
        [Route("GetStoryDetailByWishlist")]
        [Produces(typeof(ApiResponse<WishlistResponseDTO>))]
        public async Task<IActionResult> GetStory([FromQuery] String ProviderToken, Int64 StoryId)
        {
            WishlistResponseDTO? userStory = await _serviceManager.WishlistService.GetStoryByWishlist(ProviderToken, StoryId);

            ApiResponse<WishlistResponseDTO?> response = new ApiResponse<WishlistResponseDTO?>()
            {
                Success = true,
                StatusCode = StatusCodes.Status200OK,
                Message = "Success",
                Data = userStory
            };

            return Ok(response);
        }

        [HttpDelete]
        [Route("DeleteStoryWishlist")]
        [Produces(typeof(ApiResponse<IEnumerable<WishlistResponseDTO>>))]
        public async Task<IActionResult> DeleteStoryWishlist([FromBody] StoryUserWishlistCreate request)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            WishlistResponseDTO responseDto = await _serviceManager.WishlistService.DeleteStoryWishList(request);


            ApiResponse<WishlistResponseDTO> response = new ApiResponse<WishlistResponseDTO>()
            {
                Success = true,
                StatusCode = StatusCodes.Status200OK,
                Message = "Success",

            };
            return Ok(response);
        }

    }
}
