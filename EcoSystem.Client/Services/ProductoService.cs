using EcoSystem.Client.Models;

namespace EcoSystem.Client.Services;

public class ProductoService
{
    private const string Endpoint = "api/Productos";
    private readonly ApiService _apiService;

    public ProductoService(ApiService apiService)
    {
        _apiService = apiService;
    }

    public Task<List<Producto>> GetProductosAsync()
    {
        return _apiService.GetListAsync<Producto>(Endpoint);
    }

    public Task<Producto?> GetProductoByIdAsync(int id)
    {
        return _apiService.GetAsync<Producto>($"{Endpoint}/{id}");
    }

    public Task<Producto?> CreateProductoAsync(Producto producto)
    {
        return _apiService.PostAsync<Producto, Producto>(Endpoint, producto);
    }

    public Task UpdateProductoAsync(int id, Producto producto)
    {
        return _apiService.PutAsync($"{Endpoint}/{id}", producto);
    }

    public Task DeleteProductoAsync(int id)
    {
        return _apiService.DeleteAsync($"{Endpoint}/{id}");
    }
}