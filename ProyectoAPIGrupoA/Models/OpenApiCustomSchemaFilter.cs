using Microsoft.OpenApi.Any;
using Microsoft.OpenApi.Models;
using ProyectoAPIGrupoA.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using System;

public class OpenApiCustomSchemaFilter : ISchemaFilter, IOperationFilter
{
    public void Apply(OpenApiSchema schema, SchemaFilterContext context)
    {
        if (context.Type == typeof(gamePlayerName))
        {
            schema.Properties = new Dictionary<string, OpenApiSchema>
            {
                {
                    "player",
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
        //context.SchemaRepository.Schemas.Remove("VoteModel");
    }

    public void Apply(OpenApiOperation operation, OperationFilterContext context)
    {
        if (context.ApiDescription.HttpMethod == "GET")
        {

            // Ejemplo personalizado para un parámetro "name" en un método GET
            var parameterName = "name";
            var exampleValue = "EjemploValor";

            if (operation.Parameters != null)
            {
                var parameter = operation.Parameters.FirstOrDefault(p => p.Name == parameterName);
                if (parameter != null)
                {
                    if (parameter.Schema != null && parameter.Schema.Properties != null)
                    {
                        if (parameter.Schema.Properties.ContainsKey("name"))
                        {
                            parameter.Schema.Properties["name"].Example = new OpenApiString(exampleValue);
                        }
                    }
                }
            }

        }
        
    }
}



