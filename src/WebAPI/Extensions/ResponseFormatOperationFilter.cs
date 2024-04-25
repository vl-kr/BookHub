using Microsoft.OpenApi.Any;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace WebApi.Extensions;

public sealed class ResponseFormatOperationFilter : IOperationFilter
{
    public void Apply(OpenApiOperation operation, OperationFilterContext context)
    {
        operation.Parameters ??= new List<OpenApiParameter>();

        var header = new OpenApiParameter
        {
            Name = "responseFormat",
            In = ParameterLocation.Query,
            Description = "Choose response format",
            Required = false,
            Schema = new OpenApiSchema
            {
                Type = "String",
                Enum = new List<IOpenApiAny> { new OpenApiString("json"), new OpenApiString("xml") }
            }
        };

        operation.Parameters.Add(header);
    }
}
