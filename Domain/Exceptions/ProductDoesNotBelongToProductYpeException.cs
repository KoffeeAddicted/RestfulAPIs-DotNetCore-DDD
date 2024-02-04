using Domain.Exceptions.Base;

namespace Domain.Exceptions;

public sealed class ProductDoesNotBeInt64ToProductYpeException(Int64 productId, Int64 productTypeId) : BadRequestException(
    $"The product with the identifier {productId} does not beInt64 to the product type with the identifier {productTypeId}");