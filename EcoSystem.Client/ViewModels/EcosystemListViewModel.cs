using CommunityToolkit.Mvvm.ComponentModel;
using EcoSystem.Client.Models;
using EcoSystem.Client.Services;

namespace EcoSystem.Client.ViewModels;

public partial class EcosystemListViewModel : ObservableObject
{
    private readonly EcosystemService _ecosystemService;

    [ObservableProperty]
    private List<Ecosystem> ecosystems = new();

    public EcosystemListViewModel(EcosystemService ecosystemService)
    {
        _ecosystemService = ecosystemService;
    }
}