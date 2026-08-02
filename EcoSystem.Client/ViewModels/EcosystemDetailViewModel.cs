using CommunityToolkit.Mvvm.ComponentModel;
using EcoSystem.Client.Models;

namespace EcoSystem.Client.ViewModels;

public partial class EcosystemDetailViewModel : ObservableObject
{
    [ObservableProperty]
    private Ecosystem? selectedEcosystem;
}