using EcoSystem.Business.Interfaces;
using EcoSystem.Data.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EcoSystem.API.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class ProductosController : ControllerBase
{
    private readonly IProductoService _productoService;

    public ProductosController(IProductoService productoService)
    {
        _productoService = productoService;
    }

    // Administradores y clientes pueden consultar productos
    [HttpGet]
    [Authorize(Roles = "Administrador,Cliente")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult<IEnumerable<Producto>>> GetProductos()
    {
        var productos = await _productoService.GetProductosAsync();

        return Ok(productos);
    }

    // Administradores y clientes pueden consultar el detalle
    [HttpGet("{id}")]
    [Authorize(Roles = "Administrador,Cliente")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<Producto>> GetProducto(int id)
    {
        var producto = await _productoService.GetProductoByIdAsync(id);

        if (producto is null)
        {
            return NotFound();
        }

        return Ok(producto);
    }

    // Solo administrador
    [HttpPost]
    [Authorize(Roles = "Administrador")]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<Producto>> PostProducto(
        Producto producto)
    {
        var nuevoProducto =
            await _productoService.CreateProductoAsync(producto);

        return CreatedAtAction(
            nameof(GetProducto),
            new { id = nuevoProducto.Id },
            nuevoProducto);
    }

    // Solo administrador
    [HttpPut("{id}")]
    [Authorize(Roles = "Administrador")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> PutProducto(
        int id,
        Producto producto)
    {
        var actualizado =
            await _productoService.UpdateProductoAsync(id, producto);

        if (!actualizado)
        {
            return BadRequest();
        }

        return NoContent();
    }

    // Solo administrador
    [HttpDelete("{id}")]
    [Authorize(Roles = "Administrador")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> DeleteProducto(int id)
    {
        var eliminado =
            await _productoService.DeleteProductoAsync(id);

        if (!eliminado)
        {
            return NotFound();
        }

        return NoContent();
    }
}