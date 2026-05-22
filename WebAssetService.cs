using System.Net.Http.Json;
using MyMauiApp.Models;
using MyMauiApp.Services;
using MyMauiApp.Shared;

namespace MyMauiApp;

public class WebAssetService : IAssetService
{
    private readonly HttpClient _httpClient;
    private readonly IToastService _toastService;
    private const int DefaultTimeoutSeconds = 15;

    public WebAssetService(HttpClient httpClient, IToastService toastService)
    {
        _httpClient = httpClient;
        _toastService = toastService;
    }

    private async Task<T?> SendRequestAsync<T>(Func<CancellationToken, Task<T>> requestFunc)
    {
        using var cts = new CancellationTokenSource(TimeSpan.FromSeconds(DefaultTimeoutSeconds));
        try
        {
            return await requestFunc(cts.Token);
        }
        catch (OperationCanceledException)
        {
            _toastService.ShowToast("The request timed out. Please try again.");
            return default;
        }
        catch (HttpRequestException ex)
        {
            _toastService.ShowToast($"Server error: {ex.StatusCode ?? System.Net.HttpStatusCode.ServiceUnavailable}");
            return default;
        }
        catch (Exception ex)
        {
            _toastService.ShowToast($"An unexpected error occurred: {ex.Message}");
            return default;
        }
    }

    public async Task<List<Asset>> GetAssetsAsync()
    {
        var result = await SendRequestAsync(ct => _httpClient.GetFromJsonAsync<List<Asset>>("api/assets", ct));
        return result ?? new List<Asset>();
    }

    public async Task<int> SaveAssetAsync(Asset asset)
    {
        var result = await SendRequestAsync<int>(async ct =>
        {
            HttpResponseMessage response;
            if (asset.Id != 0)
            {
                response = await _httpClient.PutAsJsonAsync($"api/assets/{asset.Id}", asset, ct);
            }
            else
            {
                response = await _httpClient.PostAsJsonAsync("api/assets", asset, ct);
            }

            if (!response.IsSuccessStatusCode)
            {
                _toastService.ShowToast($"Failed to save asset: {response.StatusCode}");
                return 0;
            }
            return 1;
        });

        return result ?? 0;
    }

    public async Task DeleteAssetAsync(Asset asset)
    {
        await SendRequestAsync<bool>(async ct =>
        {
            var response = await _httpClient.DeleteAsync($"api/assets/{asset.Id}", ct);
            if (!response.IsSuccessStatusCode)
            {
                _toastService.ShowToast($"Failed to delete asset: {response.StatusCode}");
            }
            return response.IsSuccessStatusCode;
        });
    }

    public async Task<List<User>> GetUsersAsync()
    {
        var result = await SendRequestAsync(ct => _httpClient.GetFromJsonAsync<List<User>>("api/users", ct));
        return result ?? new List<User>();
    }

    public async Task<int> SaveUserAsync(User user)
    {
        var result = await SendRequestAsync<int>(async ct =>
        {
            HttpResponseMessage response;
            if (user.Id != 0)
            {
                response = await _httpClient.PutAsJsonAsync($"api/users/{user.Id}", user, ct);
            }
            else
            {
                response = await _httpClient.PostAsJsonAsync("api/users", user, ct);
            }

            if (!response.IsSuccessStatusCode)
            {
                _toastService.ShowToast($"Failed to save user: {response.StatusCode}");
                return 0;
            }
            return 1;
        });

        return result ?? 0;
    }
}