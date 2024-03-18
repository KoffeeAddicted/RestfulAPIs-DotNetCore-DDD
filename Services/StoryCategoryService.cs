using AutoMapper;
using Contracts;
using Contracts.DTOs.Stories;
using Domain.Entities;
using Domain.RepositoryInterfaces;
using Services.Absractions;
using Services.DTOs.StoriyCategories;

namespace Services;

public class StoryCategoryService : IStoryCategoryService
{
    private readonly IRepositoryManager _repositoryManager;
    private readonly IMapper _mapper;

    public StoryCategoryService(IRepositoryManager repositoryManager,
        IMapper mapper)
    {
        _repositoryManager = repositoryManager;
        _mapper = mapper;
    }

    public async Task<IEnumerable<StoryCategoryResponseDTO>> GetAllAsync()
    {
        IEnumerable<StoryCategory> storyCategory = await _repositoryManager.StoryCategoryRepository.GetAllAsync();

        IEnumerable<StoryCategoryResponseDTO> response = _mapper.Map<IEnumerable<StoryCategory>, IEnumerable<StoryCategoryResponseDTO>>(storyCategory);

        return response;
    }
}