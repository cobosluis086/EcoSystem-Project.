using System.Net;
using System.Net.Http.Json;
using EcoSystem.Client.Models;
using Microsoft.JSInterop;

namespace EcoSystem.Client.Services;

public class AuthService
{
    private readonly HttpClient _httpClient;
    private readonly IJSRuntime _jsRuntime;

    public AuthService(
        HttpClient httpClient,
        IJSRuntime jsRuntime)
    {
        _httpClient = httpClient;
        _jsRuntime = jsRuntime;
    }

    public async Task<(bool Success, string Message)> LoginAsync(
        LoginRequest request)
    {
        try
        {
            var response = await _httpClient.PostAsJsonAsync(
                "api/Auth/login",
                request);

            if (response.StatusCode == HttpStatusCode.Unauthorized)
            {
                return (
                    false,
                    "Correo electrónico o contraseña incorrectos."
                );
            }

            if (!response.IsSuccessStatusCode)
            {
                return (
                    false,
                    $"No fue posible iniciar sesión. Código: {(int)response.StatusCode}"
                );
            }

            var loginResponse =
                await response.Content.ReadFromJsonAsync<LoginResponse>();

            if (loginResponse is null ||
                string.IsNullOrWhiteSpace(loginResponse.Token))
            {
                return (
                    false,
                    "La API no devolvió un token válido."
                );
            }

            await _jsRuntime.InvokeVoidAsync(
                "localStorage.setItem",
                "authToken",
                loginResponse.Token);

            await _jsRuntime.InvokeVoidAsync(
                "localStorage.setItem",
                "userName",
                loginResponse.Nombre);

            await _jsRuntime.InvokeVoidAsync(
                "localStorage.setItem",
                "userRole",
                loginResponse.Rol);

            await _jsRuntime.InvokeVoidAsync(
                "localStorage.setItem",
                "tokenExpiration",
                loginResponse.Expiration.ToString("O"));

            return (
                true,
                "Inicio de sesión correcto."
            );
        }
        catch (HttpRequestException)
        {
            return (
                false,
                "No fue posible conectar con la API."
            );
        }
        catch (Exception ex)
        {
            return (
                false,
                $"Ocurrió un error al iniciar sesión: {ex.Message}"
            );
        }
    }

    public async Task<string?> GetTokenAsync()
    {
        return await _jsRuntime.InvokeAsync<string?>(
            "localStorage.getItem",
            "authToken");
    }

    public async Task<string> GetUserNameAsync()
    {
        return await _jsRuntime.InvokeAsync<string?>(
            "localStorage.getItem",
            "userName") ?? string.Empty;
    }

    public async Task<string> GetUserRoleAsync()
    {
        return await _jsRuntime.InvokeAsync<string?>(
            "localStorage.getItem",
            "userRole") ?? string.Empty;
    }

    public async Task<bool> IsAuthenticatedAsync()
    {
        var token = await GetTokenAsync();

        if (string.IsNullOrWhiteSpace(token))
        {
            return false;
        }

        var expirationText =
            await _jsRuntime.InvokeAsync<string?>(
                "localStorage.getItem",
                "tokenExpiration");

        if (!DateTime.TryParse(
                expirationText,
                out var expiration))
        {
            await LogoutAsync();
            return false;
        }

        if (expiration.ToUniversalTime() <= DateTime.UtcNow)
        {
            await LogoutAsync();
            return false;
        }

        return true;
    }

    public async Task<bool> IsAdminAsync()
    {
        var role = await GetUserRoleAsync();

        return string.Equals(
            role,
            "Administrador",
            StringComparison.OrdinalIgnoreCase);
    }

    public async Task<bool> IsClientAsync()
    {
        var role = await GetUserRoleAsync();

        return string.Equals(
            role,
            "Cliente",
            StringComparison.OrdinalIgnoreCase);
    }

    public async Task LogoutAsync()
    {
        await _jsRuntime.InvokeVoidAsync(
            "localStorage.removeItem",
            "authToken");

        await _jsRuntime.InvokeVoidAsync(
            "localStorage.removeItem",
            "userName");

        await _jsRuntime.InvokeVoidAsync(
            "localStorage.removeItem",
            "userRole");

        await _jsRuntime.InvokeVoidAsync(
            "localStorage.removeItem",
            "tokenExpiration");
    }
}