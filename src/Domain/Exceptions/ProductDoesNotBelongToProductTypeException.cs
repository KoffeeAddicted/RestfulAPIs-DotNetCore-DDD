using Domain.Exceptions.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Exceptions
{
    public sealed class ProductDoesNotBelongToProductTypeException : BadRequestException
    {
        public ProductDoesNotBelongToProductTypeException(long productId, long productTypeId)
        : base($"The product with the identifier {productId} does not belong to the product type with the identifier {productTypeId}")
        {
        }
    }
}
