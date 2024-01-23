using Domain.Exceptions.Base;

namespace Domain.Exceptions;

public sealed class ProductDoesNotBelongToProductYpeException(Int64 productId, Int64 productTypeId) : BadRequestException(
    $"The product with the identifier {productId} does not belong to the product type with the identifier {productTypeId}");