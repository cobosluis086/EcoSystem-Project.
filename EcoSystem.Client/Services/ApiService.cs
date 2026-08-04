using System.Net.Http.Headers;
using System.Net.Http.Json;
using Microsoft.JSInterop;

namespace EcoSystem.Client.Services;

public class ApiService
{
    private readonly HttpClient _httpClient;
    private readonly IJSRuntime _jsRuntime;

    public ApiService(
        HttpClient httpClient,
        IJSRuntime jsRuntime)
    {
        _httpClient = httpClient;
        _jsRuntime = jsRuntime;
    }

    private async Task ConfigureAuthorizationAsync()
    {
        var token = await _jsRuntime.InvokeAsync<string?>(
            "localStorage.getItem",
            "authToken");

        _httpClient.DefaultRequestHeaders.Authorization = null;

        if (!string.IsNullOrWhiteSpace(token))
        {
            _httpClient.DefaultRequestHeaders.Authorization =
                new AuthenticationHeaderValue("Bearer", token);
        }
    }

    public async Task<List<T>> GetListAsync<T>(string endpoint)
    {
        await ConfigureAuthorizationAsync();

        return await _httpClient.GetFromJsonAsync<List<T>>(endpoint)
               ?? new List<T>();
    }

    public async Task<T?> GetAsync<T>(string endpoint)
    {
        await ConfigureAuthorizationAsync();

        return await _httpClient.GetFromJsonAsync<T>(endpoint);
    }

    public async Task<TResponse?> PostAsync<TRequest, TResponse>(
        string endpoint,
        TRequest data)
    {
        await ConfigureAuthorizationAsync();

        var response = await _httpClient.PostAsJsonAsync(
            endpoint,
            data);

        response.EnsureSuccessStatusCode();

        return await response.Content
            .ReadFromJsonAsync<TResponse>();
    }

    public async Task PutAsync<T>(
        string endpoint,
        T data)
    {
        await ConfigureAuthorizationAsync();

        var response = await _httpClient.PutAsJsonAsync(
            endpoint,
            data);

        response.EnsureSuccessStatusCode();
    }

    public async Task DeleteAsync(string endpoint)
    {
        await ConfigureAuthorizationAsync();

        var response = await _httpClient.DeleteAsync(endpoint);

        response.EnsureSuccessStatusCode();
    }
}