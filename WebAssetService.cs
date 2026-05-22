using System.Net.Http.Json;
using MyMauiApp.Models;
using MyMauiApp.Services;

namespace MyMauiApp;

public class WebAssetService : IAssetService
{
    private readonly HttpClient _httpClient;

    public WebAssetService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<List<Asset>> GetAssetsAsync()
    {
        // Fetches the list of assets from the server
        return await _httpClient.GetFromJsonAsync<List<Asset>>("api/assets") ?? new List<Asset>();
    }

    public async Task<int> SaveAssetAsync(Asset asset)
    {
        HttpResponseMessage response;
        if (asset.Id != 0)
        {
            // Update existing asset via PUT
            response = await _httpClient.PutAsJsonAsync($"api/assets/{asset.Id}", asset);
        }
        else
        {
            // Create new asset via POST
            response = await _httpClient.PostAsJsonAsync("api/assets", asset);
        }

        return response.IsSuccessStatusCode ? 1 : 0;
    }

    public async Task DeleteAssetAsync(Asset asset)
    {
        await _httpClient.DeleteAsync($"api/assets/{asset.Id}");
    }

    public async Task<List<User>> GetUsersAsync()
    {
        return await _httpClient.GetFromJsonAsync<List<User>>("api/users") ?? new List<User>();
    }

    public async Task<int> SaveUserAsync(User user)
    {
        HttpResponseMessage response;
        if (user.Id != 0)
        {
            response = await _httpClient.PutAsJsonAsync($"api/users/{user.Id}", user);
        }
        else
        {
            response = await _httpClient.PostAsJsonAsync("api/users", user);
        }

        return response.IsSuccessStatusCode ? 1 : 0;
    }
}