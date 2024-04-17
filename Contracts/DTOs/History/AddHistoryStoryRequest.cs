using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contracts.DTOs.History
{
    public class AddHistoryStoryRequest
    {
        public String ProviderToken { get; set; }
        public Int64 StoryId { get; set; }
    }
}
