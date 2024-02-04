namespace Domain.Exceptions.Base;

public class ValidatePropertyException<TEntity> : NotFoundException
{
    public ValidatePropertyException(string propertyName)
        : base($"Property '{propertyName}' does not exist on entity '{typeof(TEntity).Name}'.")
    {
    }
}