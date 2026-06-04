using Scalar.AspNetCore;
using Datos;
using Microsoft.EntityFrameworkCore;
using Datos.Interfaces;
using Datos.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Conexion SQL Server
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(
        builder.Configuration.GetConnectionString("DefaultConnection")
    )
);

builder.Services.AddScoped<ICategoriaRepository, CategoriaRepository>();
builder.Services.AddControllers()
.AddJsonOptions(options =>
{
    options.JsonSerializerOptions.ReferenceHandler =
        System.Text.Json.Serialization.ReferenceHandler.IgnoreCycles;
});

// Swagger
builder.Services.AddOpenApi();

var app = builder.Build();

// OpenAPI
app.MapOpenApi();

// Scalar
app.MapScalarApiReference();

app.Run();