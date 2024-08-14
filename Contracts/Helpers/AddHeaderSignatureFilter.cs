using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace Contracts.Helpers;

public class AddHeaderSignatureFilter : IOperationFilter
{
    public void Apply(OpenApiOperation operation, OperationFilterContext context)
    {
        operation.Parameters ??= new List<OpenApiParameter>();

        operation.Parameters.Add(new OpenApiParameter
        {
            Name = "Signature",
            In = ParameterLocation.Header,
            Required = true,
            Description = "Add signature",
            Schema = new OpenApiSchema
            {
                Type = "string"
            }
        });
    }
}