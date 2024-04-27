using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contracts.DTOs.Comment
{
    public class RequestAddCommentDTO
    {
        public String ProviderToken { get; set; }
        public Int64 StoryId { get; set; }
        public String Message { get; set; }
        public Int64 Rating { get; set; }
    }
}
