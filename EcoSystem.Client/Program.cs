using EcoSystem.Client;
using EcoSystem.Client.Services;
using EcoSystem.Client.ViewModels;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

var builder = WebAssemblyHostBuilder.CreateDefault(args);

builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped(_ =>
    new HttpClient
    {
        BaseAddress = new Uri("https://ecosystem-project-4hpi.onrender.com/")
    });

builder.Services.AddScoped<ApiService>();
builder.Services.AddScoped<ProductoService>();
builder.Services.AddScoped<AuthService>();

builder.Services.AddScoped<ProductoListViewModel>();
builder.Services.AddScoped<ProductoDetailViewModel>();

await builder.Build().RunAsync();