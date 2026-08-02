using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using EcoSystem.Client.Models;
using EcoSystem.Client.Services;

namespace EcoSystem.Client.ViewModels;

public partial class ProductoListViewModel : ObservableObject
{
    private readonly ProductoService _productoService;

    [ObservableProperty]
    private List<Producto> productos = new();

    [ObservableProperty]
    private bool isLoading;

    [ObservableProperty]
    private string errorMessage = string.Empty;

    public ProductoListViewModel(ProductoService productoService)
    {
        _productoService = productoService;
    }

    [RelayCommand]
    public async Task LoadProductosAsync()
    {
        try
        {
            IsLoading = true;
            ErrorMessage = string.Empty;

            Productos = await _productoService.GetProductosAsync();
        }
        catch (Exception ex)
        {
            ErrorMessage = $"No se pudieron cargar los productos: {ex.Message}";
        }
        finally
        {
            IsLoading = false;
        }
    }
}