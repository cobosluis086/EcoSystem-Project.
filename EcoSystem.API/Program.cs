using EcoSystem.Data.Data;
using Microsoft.EntityFrameworkCore;
using EcoSystem.Business.Interfaces;
using EcoSystem.Business.Services;

var builder = WebApplication.CreateBuilder(args);

var connectionString =
    builder.Configuration.GetConnectionString("DefaultConnection")
    ?? throw new InvalidOperationException(
        "No se encontró la cadena de conexión DefaultConnection."
    );

// Base de datos PostgreSQL de Supabase
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseNpgsql(connectionString)
);

// Servicios
builder.Services.AddScoped<IProductoService, ProductoService>();

// Controladores y Swagger
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Swagger habilitado en Render
app.UseSwagger();

app.UseSwaggerUI(options =>
{
    options.SwaggerEndpoint(
        "/swagger/v1/swagger.json",
        "EcoSystem API v1"
    );
});

// Al abrir la dirección principal, redirige a Swagger
app.MapGet("/", () => Results.Redirect("/swagger"));

// Controladores
app.MapControllers();

app.Run();