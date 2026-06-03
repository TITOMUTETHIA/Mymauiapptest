using System.Net.Http.Json;
using MyMauiApp.Models;
using MyMauiApp.Shared;

namespace MyMauiApp.Services;

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

    private async Task<ServiceResponse<T>> SendRequestAsync<T>(Func<CancellationToken, Task<ServiceResponse<T>>> requestFunc)
    {
        using var cts = new CancellationTokenSource(TimeSpan.FromSeconds(DefaultTimeoutSeconds));
        try
        {
            return await requestFunc(cts.Token);
        }
        catch (OperationCanceledException)
        {
            var msg = "The request timed out. Please try again.";
            _toastService.ShowToast(msg);
            return ServiceResponse<T>.Fail(msg);
        }
        catch (HttpRequestException ex)
        {
            var msg = $"Server error: {ex.StatusCode ?? System.Net.HttpStatusCode.ServiceUnavailable}";
            _toastService.ShowToast(msg);
            return ServiceResponse<T>.Fail(msg);
        }
        catch (Exception ex)
        {
            var msg = $"An unexpected error occurred: {ex.Message}";
            _toastService.ShowToast(msg);
            return ServiceResponse<T>.Fail(msg);
        }
    }

    public async Task<ServiceResponse<List<Asset>>> GetAssetsAsync()
    {
        return await SendRequestAsync(async ct => 
            ServiceResponse<List<Asset>>.Ok(await _httpClient.GetFromJsonAsync<List<Asset>>("api/assets", ct) ?? new()));
    }

    public async Task<ServiceResponse<int>> SaveAssetAsync(Asset asset)
    {
        return await SendRequestAsync(async ct =>
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
                var error = await response.Content.ReadAsStringAsync();
                var msg = string.IsNullOrWhiteSpace(error) ? $"Failed to save asset: {response.StatusCode}" : error;
                _toastService.ShowToast(msg);
                return ServiceResponse<int>.Fail(msg);
            }
            return ServiceResponse<int>.Ok(1);
        });
    }

    public async Task<ServiceResponse<bool>> DeleteAssetAsync(Asset asset)
    {
        return await SendRequestAsync(async ct =>
        {
            var response = await _httpClient.DeleteAsync($"api/assets/{asset.Id}", ct);
            if (!response.IsSuccessStatusCode)
            {
                var error = await response.Content.ReadAsStringAsync();
                return ServiceResponse<bool>.Fail(error);
            }
            return ServiceResponse<bool>.Ok(true);
        });
    }

    public async Task<ServiceResponse<List<User>>> GetUsersAsync()
    {
        return await SendRequestAsync(async ct => 
            ServiceResponse<List<User>>.Ok(await _httpClient.GetFromJsonAsync<List<User>>("api/users", ct) ?? new()));
    }

    public async Task<ServiceResponse<int>> SaveUserAsync(User user)
    {
        return await SendRequestAsync(async ct =>
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
                var error = await response.Content.ReadAsStringAsync();
                _toastService.ShowToast(error);
                return ServiceResponse<int>.Fail(error);
            }
            return ServiceResponse<int>.Ok(1);
        });
    }
}