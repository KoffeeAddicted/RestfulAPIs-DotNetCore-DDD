using Contracts;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Services.Absractions;
using Contracts.DTOs.Wishlist;
using Contracts.DTOs.Stories;
using System.ComponentModel.DataAnnotations;
using Domain.Entities;
using Contracts.DTOs.Comment;

namespace Presentation.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CommentController : ControllerBase
    {
        private readonly IServiceManager _serviceManager;

        public CommentController(IServiceManager serviceManager)
        {
            _serviceManager = serviceManager;
        }

        [HttpPost]
        [Route("AddCommentStories")]
        [Produces(typeof(ApiResponse<Comment>))]
        public async Task<IActionResult> AddCommentStories([FromBody] RequestAddCommentDTO request)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            Comment responseDto = await _serviceManager.CommentService.addStoryCommentByUser(request);


            ApiResponse<Comment> response = new ApiResponse<Comment>()
            {
                Success = true,
                StatusCode = StatusCodes.Status200OK,
                Message = "Success",

            };
            return Ok(response);
        }


        [HttpGet]
        [Route("GetCommentStories")]
        [Produces(typeof(ApiResponse<IEnumerable<CommentResponseDTO>>))]
        public async Task<IActionResult> GetCommentStories([FromQuery] Int64 StoryId )
        {
            IEnumerable<CommentResponseDTO> comments = await _serviceManager.CommentService.getStoriesComment(StoryId);

            ApiResponse<IEnumerable<CommentResponseDTO>> response = new ApiResponse<IEnumerable<CommentResponseDTO>>()
            {
                Success = true,
                StatusCode = StatusCodes.Status200OK,
                Message = "Success",
                Data = comments
            };

            return Ok(response);
        }
    }
}
