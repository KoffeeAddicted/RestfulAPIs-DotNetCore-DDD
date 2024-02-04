using System.Reflection;
using Domain.Exceptions.Base;

namespace Contracts.Helpers;

public static class ValidatePropertyHelper
{
    public static string ValidateProperty<TEntity>(string propertyName)
    {
        // Get the type of TEntity
        Type entityType = typeof(TEntity);

        // Check if the property exists on the entity type
        PropertyInfo property = entityType.GetProperty(propertyName);
        if (property == null)
        {
            throw new ValidatePropertyException<TEntity>(propertyName);
        }

        // Return the name of the property
        return property.Name;
    }
}