using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Contracts.DTOs.Comment;
using Contracts.DTOs.Wishlist;
using Domain.Entities;

namespace Services.Absractions
{
    public interface ICommentService
    {
        Task<IEnumerable<CommentResponseDTO>> getStoriesComment(Int64 StroryId);
        Task<Comment> addStoryCommentByUser(RequestAddCommentDTO requestBody);
    }
}
