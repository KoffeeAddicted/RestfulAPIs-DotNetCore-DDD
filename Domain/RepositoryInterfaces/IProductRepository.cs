using Domain.Entities;

namespace Domain.RepositoryInterfaces;

public interface IProductRepository
{
    Task<IEnumerable<Product>> GetAllAsync(CancellationToken cancellationToken = default);
    Task<Product> GetByIdAsync(Int64 productId, CancellationToken cancellationToken = default);
    void Insert(Product product);
    void Update(Product product);
    void Remove(Product productId);
}