using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.OpenApi.Models;
using Newtonsoft.Json.Serialization;
using ProyectoAPIGrupoA.Models;
using System.Reflection;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);
builder.Host.UseSystemd();
// Add services to the container.


builder.Services.AddControllers();
builder.Services.AddControllers().AddJsonOptions(options =>
options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter()));
builder.Services.AddControllers().Services.AddControllers()
                    .AddNewtonsoftJson(options =>
                    {
                        options.SerializerSettings.ContractResolver =
                                                     new DefaultContractResolver();
                    }); ;
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddCors(options =>
{
    options.AddPolicy("GetAllPolicy",
      builder =>
      {
          builder.AllowAnyOrigin()
                 .AllowAnyHeader()
                 .AllowAnyMethod();//PUT, PATCH, GET, DELETE
      });
});
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Version = "v1",
        Title = "ContaminaDOS API",
        Description = "Academic project used to learn RESTful APIs",

    });
    var xmlFilename = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFilename));
    options.EnableAnnotations();
    options.SchemaFilter<OpenApiCustomSchemaFilter>();
});

var app = builder.Build();
app.UseForwardedHeaders(new ForwardedHeadersOptions
{
    ForwardedHeaders = ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto
});
app.UseSwagger(c =>
    {
        c.RouteTemplate = "api-docs/swagger/{documentname}/swagger.json";

    });
app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/api-docs/swagger/v1/swagger.json", "ContaminaDOS");
        c.RoutePrefix = "api-docs";


    });
app.MapGet("/", () => {
    app.Logger.LogInformation("Information - Hello World");
    app.Logger.LogWarning("Warning - Hello World");
    app.Logger.LogError("Error - Hello World");
    app.Logger.LogCritical("Critical - Hello World");
    return "Hello World!";
});

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    app.UseDeveloperExceptionPage();
}

app.UseHsts(); // Habilita HTTP Strict Transport Security (HSTS) para solicitudes HTTPS

//app.UseHttpsRedirection();

app.UseAuthorization();

app.UseRouting();

app.UseCors("GetAllPolicy");

app.MapWhen(context => context.Request.IsHttps, app =>
{
   // app.UseHttpsRedirection(); // Redirige solicitudes HTTPS
    app.UseEndpoints(endpoints =>
    {
        endpoints.MapControllers();
    });
});

app.MapWhen(context => !context.Request.IsHttps, app =>
{
    app.UseEndpoints(endpoints =>
    {
        endpoints.MapControllers();
    });
});

app.Run();
