using Contracts.DTOs.Product;
using Contracts.DTOs.ProductType;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Abstractions.ServiceInterfaces
{
    public interface IProductTypeService
    {
        Task<IEnumerable<ProductTypeView>> GetProductTypesAsync(CancellationToken cancellationToken = default);
        Task<ProductTypeView> GetProductypeByIdAsync(long id, CancellationToken cancellationToken = default);
        Task<ProductTypeView> CreateProductTypeASync(ProductTypeCreate product, CancellationToken cancellationToken = default);
        Task<ProductTypeView> UpdateProductTypeAsync(ProductTypeCreate product, CancellationToken cancellationToken = default);
        Task<ProductTypeView> DeleteProductTypeAsync(long id, CancellationToken cancellationToken = default);
    }
}
