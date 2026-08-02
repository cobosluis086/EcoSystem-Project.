using System.Net.Http.Json;

namespace EcoSystem.Client.Services;

public class ApiService
{
    private readonly HttpClient _httpClient;

    public ApiService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<List<T>> GetListAsync<T>(string endpoint)
    {
        return await _httpClient.GetFromJsonAsync<List<T>>(endpoint)
               ?? new List<T>();
    }

    public Task<T?> GetAsync<T>(string endpoint)
    {
        return _httpClient.GetFromJsonAsync<T>(endpoint);
    }

    public async Task<TResponse?> PostAsync<TRequest, TResponse>(
        string endpoint,
        TRequest data)
    {
        var response = await _httpClient.PostAsJsonAsync(endpoint, data);

        response.EnsureSuccessStatusCode();

        return await response.Content.ReadFromJsonAsync<TResponse>();
    }

    public async Task PutAsync<T>(string endpoint, T data)
    {
        var response = await _httpClient.PutAsJsonAsync(endpoint, data);

        response.EnsureSuccessStatusCode();
    }

    public async Task DeleteAsync(string endpoint)
    {
        var response = await _httpClient.DeleteAsync(endpoint);

        response.EnsureSuccessStatusCode();
    }
}