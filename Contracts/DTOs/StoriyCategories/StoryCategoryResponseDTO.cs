using Domain.Entities;

namespace Services.DTOs.StoriyCategories;

public class StoryCategoryResponseDTO : EntityBase<Int64>
{
    public String Name { get; set; }
}