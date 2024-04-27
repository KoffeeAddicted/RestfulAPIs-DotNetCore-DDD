using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Contracts.DTOs.Users;

namespace Contracts.DTOs.Comment
{
    public class CommentResponseDTO
    {
        public String ProviderToken { get; set; }

        public Int64 StoryId { get; set; }

        public String Message { get; set; }

        public Int64 Rating { get; set; }

        public UserResponseDTO user { get; set; }
    }
}
