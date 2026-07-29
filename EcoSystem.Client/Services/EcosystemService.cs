using EcoSystem.Client.Models;

namespace EcoSystem.Client.Services;

public class EcosystemService
{
    private readonly ApiService _apiService;

    public EcosystemService(ApiService apiService)
    {
        _apiService = apiService;
    }

    public Task<List<Ecosystem>> GetEcosystemsAsync()
    {
        return Task.FromResult(new List<Ecosystem>());
    }
}