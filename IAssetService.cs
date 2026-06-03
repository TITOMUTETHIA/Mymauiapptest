using MyMauiApp.Models;

namespace MyMauiApp.Services;

public interface IAssetService
{
    Task<ServiceResponse<List<Asset>>> GetAssetsAsync(); 
    Task<ServiceResponse<int>> SaveAssetAsync(Asset asset);
    Task<ServiceResponse<bool>> DeleteAssetAsync(Asset asset);

    Task<ServiceResponse<List<User>>> GetUsersAsync();
    Task<ServiceResponse<int>> SaveUserAsync(User user);
}