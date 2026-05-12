using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using MyMauiApp.Web.Services; // Updated namespace for WebCameraService
using MyMauiApp.Shared.Services; // Updated namespace for shared services

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<Routes>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped(sp => new HttpClient { 
    BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) 
});
builder.Services.AddScoped<ICameraService, WebCameraService>(); // WebCameraService is now in MyMauiApp.Web.Services
builder.Services.AddScoped<ICartService, CartService>(); // CartService is now in MyMauiApp.Shared.Services

await builder.Build().RunAsync();