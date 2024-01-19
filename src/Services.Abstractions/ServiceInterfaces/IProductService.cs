using Contracts.DTOs.Product;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Abstractions.ServiceInterfaces
{
    public interface IProductService
    {
        Task<IEnumerable<ProductView>> GetProductsAsync(CancellationToken cancellationToken = default);
        Task<ProductView> GetProductByIdAsync(long id, CancellationToken cancellationToken = default);
        Task<ProductView> CreateProductASync (ProductCreate product, CancellationToken cancellationToken = default);
        Task<ProductView> UpdateProductAsync (ProductCreate product, CancellationToken cancellationToken = default);
        Task<ProductView> DeleteProductAsync (long id, CancellationToken cancellationToken = default);
    }
}
