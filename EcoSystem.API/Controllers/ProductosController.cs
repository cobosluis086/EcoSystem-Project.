using EcoSystem.Data.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EcoSystem.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProductosController : ControllerBase
{
    private readonly AppDbContext _context;

    public ProductosController(AppDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<IActionResult> ObtenerTodos()
    {
        var productos = await _context.Productos.ToListAsync();

        return Ok(new
        {
            mensaje = "Productos obtenidos correctamente",
            total = productos.Count,
            datos = productos
        });
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> ObtenerPorId(int id)
    {
        var producto = await _context.Productos.FindAsync(id);

        if (producto == null)
        {
            return NotFound(new { mensaje = "Producto no encontrado" });
        }

        return Ok(new { datos = producto });
    }
}