using Domain.Entities;
using Services.DTOs.StoriyCategories;

namespace Services.Absractions;

public interface IStoryCategoryService
{
    Task<IEnumerable<StoryCategoryResponseDTO>> GetAllAsync();
}