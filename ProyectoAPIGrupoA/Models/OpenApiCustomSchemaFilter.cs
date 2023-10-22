using Microsoft.OpenApi.Any;
using Microsoft.OpenApi.Models;
using ProyectoAPIGrupoA.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

public class OpenApiCustomSchemaFilter : ISchemaFilter
{
    public void Apply(OpenApiSchema schema, SchemaFilterContext context)
    {
        if (context.Type == typeof(gamePlayerName))
        {
            schema.Properties = new Dictionary<string, OpenApiSchema>
            {
                {
                    "-",
                    new OpenApiSchema
                    {
                        Type = "string",
                        MaxLength = 20,
                        MinLength = 3,
                        Example = new OpenApiString("Thanos")
                    }
                }
            };
        }
    }
}