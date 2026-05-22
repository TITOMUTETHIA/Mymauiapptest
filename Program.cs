// This file has been moved to the Web project folder to prevent 
// compilation conflicts with the MAUI mobile target.

using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using MyMauiApp;
using MyMauiApp.Shared;
using Microsoft.AspNetCore.Components.Authorization;
using MyMauiApp.Services;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<Routes>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped(sp => new HttpClient { 
    BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) 
});
builder.Services.AddScoped<ICameraService, WebCameraService>();
builder.Services.AddScoped<IToastService, WebToastService>(); // C# only toast for Web
builder.Services.AddScoped<AuthenticationStateProvider, CustomAuthenticationStateProvider>();

// You must register an IAssetService implementation here for the Web project to run.
builder.Services.AddScoped<IAssetService, WebAssetService>();

builder.Services.AddAuthorizationCore(); // Required for AuthorizeRouteView
builder.Services.AddScoped<IAuthenticationService, AuthenticationService>();

await builder.Build().RunAsync();