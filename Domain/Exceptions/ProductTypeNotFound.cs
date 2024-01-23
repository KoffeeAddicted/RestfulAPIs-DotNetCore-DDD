using Domain.Exceptions.Base;

namespace Domain.Exceptions;

public sealed class ProductTypeNotFound(Int64 productTypeId) : NotFoundException(
    $"The product type with the identifier {productTypeId} was not found.") 
{
    
}