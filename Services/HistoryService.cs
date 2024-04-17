using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AngleSharp.Browser.Dom;
using AutoMapper;
using Contracts;
using Contracts.DTOs.History;
using Contracts.DTOs.Stories;
using Contracts.DTOs.Users;
using Domain;
using Domain.Entities;
using Domain.RepositoryInterfaces;
using Microsoft.Extensions.Options;
using Services.Absractions;

namespace Services
{
    public class HistoryService : IHistoryService
    {

        private readonly IRepositoryManager _repositoryManager;
        private readonly IMapper _mapper;
        private readonly AppSettings _appSettings;

        public HistoryService(IRepositoryManager repositoryManager,
            IMapper mapper,
            IOptions<AppSettings> appSettings)
        {
            _repositoryManager = repositoryManager;
            _mapper = mapper;
            _appSettings = appSettings.Value;
        }
        public async Task<HistoryResponseDTO> AddStoryUserHistory(AddHistoryStoryRequest addHistoryStoryRequest)
        {

            History history = _mapper.Map<AddHistoryStoryRequest, History>(addHistoryStoryRequest);


            _repositoryManager.HistoryRepository.Insert(history);


            HistoryResponseDTO response = _mapper.Map<History, HistoryResponseDTO>(history);

            return response;
        }

        public async Task<IEnumerable<HistoryResponseDTO>> GetUserHistory(String ProviderToken)
        {
            IEnumerable<History> storiesHistory = await _repositoryManager.HistoryRepository.getStoriesUserHistory(ProviderToken);

            IEnumerable<HistoryResponseDTO> storiesResponse =
            _mapper.Map<IEnumerable<History>, IEnumerable<HistoryResponseDTO>>(storiesHistory);


            return storiesResponse;
        }

        public async Task<IEnumerable<HistoryResponseDTO>> DeleteUserHistories(String ProviderToken)
        {
            IEnumerable<History> histories = await _repositoryManager.HistoryRepository.getStoriesUserHistory(ProviderToken);

            _repositoryManager.HistoryRepository.Delete(histories);


            IEnumerable<HistoryResponseDTO> response = _mapper.Map<IEnumerable<History>, IEnumerable<HistoryResponseDTO>>(histories);

            return response;

        }
        

    }
}
