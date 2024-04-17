using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Contracts.DTOs.Stories;

namespace Contracts.DTOs.History
{
    public class HistoryResponseDTO
    {
        public Int64 Id { get; set; }
        public String ProviderToken { get; set; }
        public Int64 StoryId { get; set; }
        public StoryResponseDTO Story { get; set; }
    }
}

