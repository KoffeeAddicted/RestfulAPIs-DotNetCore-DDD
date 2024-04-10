using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Contracts.DTOs.Users;
using Contracts;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Services.Absractions;
using Contracts.DTOs.Wishlist;
using Contracts.DTOs.Stories;
using System.ComponentModel.DataAnnotations;

namespace Presentation.Controllers
{
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

    }
}
