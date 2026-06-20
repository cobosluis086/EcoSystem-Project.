using EcoSystem.Data.Data;
using Microsoft.EntityFrameworkCore;
using EcoSystem.Business.Interfaces;
using EcoSystem.Business.Services;

var builder = WebApplication.CreateBuilder(args);

// Base de datos (Supabase PostgreSQL)
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseNpgsql(
        builder.Configuration.GetConnectionString("DefaultConnection")
    ));
builder.Services.AddScoped<IProductoService, ProductoService>();

// Controladores
builder.Services.AddControllers();

// OpenAPI / Swagger
builder.Services.AddOpenApi();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Swagger
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();

    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

// Habilitar controladores
app.MapControllers();

app.Run();