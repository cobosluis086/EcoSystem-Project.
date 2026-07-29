using EcoSystem.Client;
using EcoSystem.Client.Services;
using EcoSystem.Client.ViewModels;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

var builder = WebAssemblyHostBuilder.CreateDefault(args);

builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

// Cliente HTTP principal
builder.Services.AddScoped(sp =>
    new HttpClient
    {
        BaseAddress = new Uri(builder.HostEnvironment.BaseAddress)
    });

// Servicios
builder.Services.AddScoped<ApiService>();
builder.Services.AddScoped<EcosystemService>();

// ViewModels
builder.Services.AddScoped<MainViewModel>();
builder.Services.AddScoped<EcosystemListViewModel>();
builder.Services.AddScoped<EcosystemDetailViewModel>();

await builder.Build().RunAsync();