using Domain.Entities;

namespace Domain.RepositoryInterfaces;

public interface IProductTypeInterface
{
    Task<IEnumerable<ProductType>> GetAllAsync(CancellationToken cancellationToken = default);
    Task<ProductType> GetByIdAsync(Int64 productTypeId, CancellationToken cancellationToken = default);
    void Insert(ProductType productType);
    void Update(ProductType productType);
    void Remove(Int64 productTypeId);
}