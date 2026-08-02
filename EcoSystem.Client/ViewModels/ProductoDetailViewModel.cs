using CommunityToolkit.Mvvm.ComponentModel;
using EcoSystem.Client.Models;
using EcoSystem.Client.Services;

namespace EcoSystem.Client.ViewModels;

public partial class ProductoDetailViewModel : ObservableObject
{
    private readonly ProductoService _productoService;

    [ObservableProperty]
    private Producto? producto;

    [ObservableProperty]
    private bool isLoading;

    [ObservableProperty]
    private string errorMessage = string.Empty;

    public ProductoDetailViewModel(ProductoService productoService)
    {
        _productoService = productoService;
    }

    public async Task LoadProductoAsync(int id)
    {
        try
        {
            IsLoading = true;
            ErrorMessage = string.Empty;

            Producto = await _productoService.GetProductoByIdAsync(id);

            if (Producto is null)
            {
                ErrorMessage = "No se encontró el producto.";
            }
        }
        catch (Exception ex)
        {
            ErrorMessage = $"No se pudo cargar el producto: {ex.Message}";
        }
        finally
        {
            IsLoading = false;
        }
    }
}