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
    private Producto nuevoProducto = new();

    [ObservableProperty]
    private bool isLoading;

    [ObservableProperty]
    private bool isSaving;

    [ObservableProperty]
    private string errorMessage = string.Empty;

    [ObservableProperty]
    private string successMessage = string.Empty;

    public ProductoListViewModel(
        ProductoService productoService)
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

            Productos =
                await _productoService.GetProductosAsync();
        }
        catch (Exception ex)
        {
            ErrorMessage =
                $"No se pudieron cargar los productos: {ex.Message}";
        }
        finally
        {
            IsLoading = false;
        }
    }

    public async Task<bool> GuardarProductoAsync()
    {
        try
        {
            IsSaving = true;
            ErrorMessage = string.Empty;
            SuccessMessage = string.Empty;

            var productoCreado =
                await _productoService.CreateProductoAsync(
                    NuevoProducto);

            if (productoCreado is null)
            {
                ErrorMessage =
                    "La API no devolvió el producto creado.";

                return false;
            }

            SuccessMessage =
                $"El producto \"{productoCreado.Nombre}\" fue registrado correctamente.";

            NuevoProducto = new Producto();

            await LoadProductosAsync();

            return true;
        }
        catch (HttpRequestException ex)
        {
            ErrorMessage =
                $"No se pudo registrar el producto: {ex.Message}";

            return false;
        }
        catch (Exception ex)
        {
            ErrorMessage =
                $"Ocurrió un error: {ex.Message}";

            return false;
        }
        finally
        {
            IsSaving = false;
        }
    }
}