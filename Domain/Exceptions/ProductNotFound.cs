using Domain.Exceptions.Base;

namespace Domain.Exceptions;

public sealed class ProductNotFound(Int64 productId) : NotFoundException(
    $"The product with the identifier {productId} was not found.") 
{
    
}