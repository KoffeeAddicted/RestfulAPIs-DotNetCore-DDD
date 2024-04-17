using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Contracts.DTOs.History;
using Contracts.DTOs.Users;

namespace Services.Absractions
{
    public interface IHistoryService
    {
        Task<HistoryResponseDTO> AddStoryUserHistory(AddHistoryStoryRequest addHistoryStoryRequest);
        Task<IEnumerable<HistoryResponseDTO>> DeleteUserHistories(String ProviderToken);
        Task<IEnumerable<HistoryResponseDTO>> GetUserHistory(String ProviderToken);
    }
}
